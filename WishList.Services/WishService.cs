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
using WishList.Shared.Notify.Notifications;

namespace WishList.Services
{
    public class WishService : BaseService, IWishService
    {
        private readonly IWishRepository wishRepository;
        private readonly IIndexService<WishModel> indexService;
        private readonly IWishQueryRepository wishQueryRepository;
        private readonly IUserQueryRepository userQueryRepository;
        private readonly IProductQueryRepository productQueryRepository;
        private readonly IUnitOfWork unitOfWork;

        public WishService(
            IWishRepository wishRepository,
            IIndexService<WishModel> indexService,
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

        public async Task Save(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            try
            {
                if (await IfUserNonExists(userId) || await IfProductNonExists(wishCreationModel))
                    return;

                var wish = await this.wishQueryRepository.GetByUser(userId);

                if (wish != null)
                {
                    await Update(wish, wishCreationModel);

                    return;
                }

                await Insert(userId, wishCreationModel);
            }
            catch (Exception ex)
            {
                AddNotification<Failure>(ex.GetExceptionMessages());
            }
        }

        public async Task Remove(int userId, int productId)
        {
            try
            {
                var wish = await this.wishQueryRepository.GetByUser(userId);

                if (wish == null)
                {
                    AddNotification<NotFound>($"Not found a wishlist from userId {userId}");

                    return;
                }

                if (!wish.Products.Select(x => x.Id).Contains(productId))
                {
                    AddNotification<NotFound>($"Product not found with id {productId} in wishlist");

                    return;
                }

                wish.Products.Remove(wish.Products.Find(x => x.Id == productId));

                await UpdateOrDeleteWishListInIndexService(wish);

                this.wishRepository.Remove(userId, productId);

                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                AddNotification<Failure>(ex.GetExceptionMessages());
            }
        }

        /// <summary>
        /// Update wishlist if has items, else delete wishlist
        /// </summary>
        /// <param name="wish"></param>
        /// <returns></returns>
        private async Task UpdateOrDeleteWishListInIndexService(WishModel wish)
        {
            if (!wish.Products.Any())
                await this.indexService.DeleteDocumentAsync(wish.Id, wish);
            else
                await this.indexService.UpdateDocumentAsync(wish.Id, wish);
        }

        /// <summary>
        /// Check if user non exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> IfUserNonExists(int userId)
        {
            if (!await this.userQueryRepository.UserExists(userId))
            {
                AddNotification<NotFound>($"User not found with id {userId}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if product non exists
        /// </summary>
        /// <param name="wishCreationModel"></param>
        /// <returns></returns>
        private async Task<bool> IfProductNonExists(IEnumerable<WishCreationModel> wishCreationModel)
        {
            var exists = false;

            foreach (var wishItem in wishCreationModel)
            {
                if (!await this.productQueryRepository.ProductExists(wishItem.IdProduct))
                {
                    AddNotification<NotFound>($"Product not found with id {wishItem.IdProduct}");

                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Check if the new items already exist in wishlist
        /// </summary>
        /// <param name="wish"></param>
        /// <param name="newWishListItems"></param>
        /// <returns></returns>
        private bool IfWishItemsExistsInWishList(WishModel wish, IEnumerable<int> newWishListItems)
        {
            var itemsAlreadyExists = wish.Products.Select(x => x.Id).Intersect(newWishListItems);

            if (itemsAlreadyExists.Any())
            {
                AddNotification<Conflict>($"The product(s) {string.Join(",", itemsAlreadyExists)} already exist in user {wish.Id} wishlist");
                return true;
            }

            return false;
        }

        private async Task Insert(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            foreach (var wishItem in wishCreationModel)
            {
                this.wishRepository.Add(Wish.Create(userId, wishItem.IdProduct));
            }

            unitOfWork.Save();

            await PrepareToInsertInIndexService(userId, wishCreationModel);
        }

        private async Task PrepareToInsertInIndexService(int userId, IEnumerable<WishCreationModel> wishCreationModel)
        {
            var products = new List<ProductModel>();

            foreach (var i in wishCreationModel)
                products.Add(await this.productQueryRepository.GetById(i.IdProduct));

            await this.indexService.IndexDocumentAsync(new WishModel
            {
                Id = userId,
                Products = products,
            });
        }

        private async Task Update(WishModel wish, IEnumerable<WishCreationModel> wishCreationModel)
        {
            var productsIds = wishCreationModel.Select(x => x.IdProduct);

            if (IfWishItemsExistsInWishList(wish, productsIds)) return;

            foreach (var wishItem in productsIds)
            {
                this.wishRepository.Add(Wish.Create(wish.Id, wishItem));
            }

            unitOfWork.Save();

            await PrepareToUpdateInIndexService(wish, wishCreationModel);
        }

        private async Task PrepareToUpdateInIndexService(WishModel wish, IEnumerable<WishCreationModel> wishCreationModel)
        {
            var products = new List<ProductModel>();

            foreach (var i in wishCreationModel)
                products.Add(await this.productQueryRepository.GetById(i.IdProduct));

            wish.Products.AddRange(products);

            await this.indexService.UpdateDocumentAsync(wish.Id, wish);
        }
    }
}
