using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Repositories.Context;

namespace WishList.Domain.Repositories
{
    public class WishRepository : IWishRepository
    {
        private readonly WishListContext context;

        public WishRepository(WishListContext context)
        {
            this.context = context;
        }

        public void Add(Wish wish)
        {
            this.context.Wishes.Add(wish);
        }

        public void Remove(int userId, int productId)
        {
            var wish = this.context.Wishes.First(x => x.UserId == userId && x.ProductId == productId);

            this.context.Wishes.Remove(wish);
        }


        public void Update(IDictionary<int,int> dataToUpdate)
        {
            foreach(var data in dataToUpdate)
            {
                var wish = this.context.Wishes.Find(data.Key);
                wish.ProductId = data.Value;

                this.context.Entry(wish).State = EntityState.Modified;
            }
        }
    }
}
