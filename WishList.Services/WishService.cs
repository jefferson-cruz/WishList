using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Models.Product;
using WishList.Models.Wish;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Exception;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class WishService : IWishService
    {
        private readonly IIndexService<WishModel> indexService;
        private readonly IWishRepository wishRepository;
        private readonly IWishQueryRepository wishQueryRepository;
        private readonly IUserQueryRepository userQueryRepository;
        private readonly IProductQueryRepository productQueryRepository;
        private readonly IUnitOfWork unitOfWork;

        public WishService(
            IIndexService<WishModel> indexService,
            IWishRepository wishRepository,
            IWishQueryRepository wishQueryRepository,
            IUserQueryRepository userQueryRepository,
            IProductQueryRepository productQueryRepository,
            IUnitOfWork unitOfWork)
        {
            this.wishRepository = wishRepository;
            this.indexService = indexService;
            this.wishQueryRepository = wishQueryRepository;
            this.userQueryRepository = userQueryRepository;
            this.productQueryRepository = productQueryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<WishModel>> Save(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            try
            {
                if (await IfUserNonExists(userId))
                    return OperationResult.NotFound<WishModel>($"UserId {userId} not found");

                var productsNonExists = await GetProductsWithNonExists(wishCreationModel);

                if (productsNonExists.Any())
                    return OperationResult.NotFound<WishModel>($"Product(s) {string.Join(", ", productsNonExists)} not found");

                var wish = await this.wishQueryRepository.GetByUser(userId);

                if (wish != null)
                    return await Update(wish, wishCreationModel);

                return await Insert(userId, wishCreationModel);
            }
            catch (Exception ex)
            {
                return OperationResult.NotFound<WishModel>(ex.GetExceptionMessages());
            }
        }

        public async Task<Result> Remove(int userId, int productId)
        {
            try
            {
                var wish = await this.wishQueryRepository.GetByUser(userId);

                if (wish == null)
                    return OperationResult.NotFound($"Not found a wishlist from userId {userId}");

                if (!wish.Products.Select(x => x.Id).Contains(productId))
                    return OperationResult.NotFound($"Product not found with id {productId} in wishlist");

                wish.Products.Remove(wish.Products.Find(x => x.Id == productId));

                await UpdateOrDeleteWishListInIndexService(wish);

                this.wishRepository.RemoveItem(userId, productId);

                unitOfWork.Save();

                return OperationResult.OK();
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError(ex.GetExceptionMessages());
            }
        }

        /// <summary>
        /// Update wishlist if has items, else delete wishlist
        /// </summary>
        /// <param name="wish"></param>
        /// <returns></returns>
        private async Task<Result> UpdateOrDeleteWishListInIndexService(WishModel wish)
        {
            if (wish.Products.Any())
                return await this.indexService.UpdateDocumentAsync(wish.Id, wish);

            return await this.indexService.DeleteDocumentAsync(wish.Id);
        }

        /// <summary>
        /// Check if user non exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> IfUserNonExists(int userId)
        {
            return !await this.userQueryRepository.UserExists(userId);
        }

        /// <summary>
        /// Check if product non exists
        /// </summary>
        /// <param name="wishCreationModel"></param>
        /// <returns></returns>
        private async Task<int[]> GetProductsWithNonExists(IEnumerable<WishCreationModel> wishCreationModel)
        {
            var nonExists = new List<int>();

            foreach (var wishItem in wishCreationModel)
                if (!await this.productQueryRepository.ProductExists(wishItem.IdProduct))
                    nonExists.Add(wishItem.IdProduct);

            return nonExists.ToArray();
        }

        /// <summary>
        /// Check if the new items already exist in wishlist
        /// </summary>
        /// <param name="wish"></param>
        /// <param name="newWishListItems"></param>
        /// <returns></returns>
        private Result IfWishItemsExistsInWishList(WishModel wish, IEnumerable<int> newWishListItems)
        {
            var itemsAlreadyExists = wish.Products.Select(x => x.Id).Intersect(newWishListItems);

            if (itemsAlreadyExists.Any())
                return OperationResult.Conflict($"The product(s) {string.Join(",", itemsAlreadyExists)} already exist in user {wish.Id} wishlist");

            return OperationResult.OK();
        }

        private async Task<Result<WishModel>> Insert(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            foreach (var wishItem in wishCreationModel)
                this.wishRepository.Add(Wish.Create(userId, wishItem.IdProduct).Value);

            unitOfWork.Save();

            var products = new List<ProductModel>();

            foreach (var i in wishCreationModel)
                products.Add(await this.productQueryRepository.GetById(i.IdProduct));

            var model = new WishModel
            {
                Id = userId,
                Products = products,
            };

            var indexResult = await this.indexService.IndexDocumentAsync(model);

            if (indexResult.Failure)
            {
                await RollbackInsert(userId, wishCreationModel);

                return OperationResult.InternalServerError<WishModel>(indexResult);
            }

            return OperationResult.Created(model);
        }

        private async Task<Result<WishModel>> Update(WishModel wish, IEnumerable<WishCreationModel> wishCreationModel)
        {
            var productsIds = wishCreationModel.Select(x => x.IdProduct);

            var existsItemsResult = IfWishItemsExistsInWishList(wish, productsIds);

            if (existsItemsResult.Failure)
                return OperationResult.NotFound<WishModel>(existsItemsResult);

            foreach (var wishItem in productsIds)
                this.wishRepository.Add(Wish.Create(wish.Id, wishItem).Value);

            unitOfWork.Save();

            var products = new List<ProductModel>();

            foreach (var i in wishCreationModel)
                products.Add(await this.productQueryRepository.GetById(i.IdProduct));

            wish.Products.AddRange(products);

            var indexResult = await this.indexService.UpdateDocumentAsync(wish.Id, wish);

            if (indexResult.Failure)
            {
                await RollbackUpdate(wish, wishCreationModel);

                return OperationResult.InternalServerError<WishModel>(indexResult);
            }

            return OperationResult.NoContent<WishModel>();
        }

        private async Task RollbackInsert(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            await this.indexService.DeleteDocumentAsync(userId);

            this.wishRepository.Remove(userId);

            this.unitOfWork.Save();
        }

        private async Task RollbackUpdate(WishModel wishModel, IEnumerable<WishCreationModel> wishCreationModel)
        {
            var wish = await this.wishQueryRepository.GetByUser(wishModel.Id);

            foreach (var wishItem in wishCreationModel)
            {
                var toRemove = wish.Products.Find(x => x.Id == wishItem.IdProduct);
                wish.Products.Remove(toRemove);
            }

            await this.indexService.UpdateDocumentAsync(wish.Id, wish);

            this.wishRepository.RemoveItems(wishModel.Id, wishCreationModel.Select(x => x.IdProduct));

            this.unitOfWork.Save();
        }
    }
}
