using System.Collections.Generic;

namespace WishList.Domain.Repositories
{
    public interface IWishRepository
    {
        /// <summary>
        /// Add a wishlist
        /// </summary>
        /// <param name="wish"></param>
        void Add(Entities.Wish wish);

        /// <summary>
        /// Remove one item from wishlist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        void RemoveItem(int userId, int productId);

        /// <summary>
        /// Remove the wishlist
        /// </summary>
        /// <param name="userId"></param>
        void Remove(int userId);

        /// <summary>
        /// Remove items from wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enumerable"></param>
        void RemoveItems(int id, IEnumerable<int> enumerable);
    }
}
