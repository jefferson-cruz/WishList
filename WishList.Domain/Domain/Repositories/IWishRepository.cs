using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishList.Domain.Repositories
{
    public interface IWishRepository
    {
        void Add(Entities.Wish wish);
        void Remove(int userId, int productId);
        void Update(IDictionary<int, int> data);
        void Remove(int userId);
        void Remove(int id, IEnumerable<int> enumerable);
    }
}
