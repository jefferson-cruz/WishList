using AutoMapper;
using System;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Models.Product;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class ProductService : IProductService
    {
        private readonly IIndexService<ProductModel> indexService;
        private readonly IProductRepository productRepository;
        private readonly IProductQueryRepository productQueryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(
            IIndexService<ProductModel> indexService,
            IProductRepository productRepository,
            IProductQueryRepository productQueryRepository,
            IUnitOfWork unitOfWork)
        {
            this.indexService = indexService;
            this.productRepository = productRepository;
            this.productQueryRepository = productQueryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductModel>> Create(ProductCreationModel productModel)
        {
            try
            {
                var productResult = Product.Create(productModel.Name);

                if (productResult.Failure) return OperationResult.BadRequest<ProductModel>(productResult);

                if (await this.productQueryRepository.ProductExists(productModel.Name))
                    return OperationResult.NotFound<ProductModel>("Product already exists");

                this.productRepository.Add(productResult.Value);

                unitOfWork.Save();

                var model = Mapper.Map<ProductModel>(productResult.Value);

                var indexResult = await indexService.IndexDocumentAsync(model);

                if (indexResult.Failure)
                {
                    await Rollback(productResult.Value);

                    return OperationResult.InternalServerError<ProductModel>(indexResult);
                }

                return OperationResult.Created(model);
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError<ProductModel>(ex);
            }
        }

        private async Task Rollback(Product product)
        {
            await this.indexService.DeleteDocumentAsync(product.Id);

            this.productRepository.Remove(product);

            this.unitOfWork.Save();
        }
    }
}
