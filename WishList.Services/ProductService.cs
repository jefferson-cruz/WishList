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
        private readonly IProductRepository productRepository;
        private readonly IProductQueryRepository productQueryRepository;
        private readonly IIndexService<ProductModel> indexService;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IProductQueryRepository productQueryRepository,
            IIndexService<ProductModel> indexService,
            IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.productQueryRepository = productQueryRepository;
            this.indexService = indexService;
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

                AddNotifications(product.Notifications);

                if (HasNotification) return null;

                this.productRepository.Add(product);

                unitOfWork.Save();

                var model = Mapper.Map<ProductModel>(product);

                await indexService.IndexDocumentAsync(model);

                return model;
            }
            catch (Exception ex)
            {
                AddNotification<Failure>(ex.GetExceptionMessages());

                return null;
            }
        }
    }
}
