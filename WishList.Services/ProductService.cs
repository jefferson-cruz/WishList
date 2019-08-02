using AutoMapper;
using System;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Models.Product;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Exception;
using WishList.Shared.Notify.Notifications;

namespace WishList.Services
{
    public class ProductService : BaseService, IProductService
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

        public async Task<ProductModel> Create(ProductCreationModel productModel)
        {
            try
            {
                if (await this.productQueryRepository.ProductExists(productModel.Name))
                {
                    AddNotification<Conflict>("Product already exists");

                    return null;
                }

                var product = Product.Create(productModel.Name);

                AddResutls(product.Results);

                if (HasResults) return null;

                this.productRepository.Add(product);

                unitOfWork.Save();

                var model = Mapper.Map<ProductModel>(product);

                await indexService.IndexDocumentAsync(model);

                if (indexService.HasResults)
                {
                    AddNotifications(indexService.Results);

                    await Rollback(product);

                    return null;
                }

                return model;
            }
            catch (Exception ex)
            {
                AddNotification<Failure>(ex.GetExceptionMessages());

                return null;
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
