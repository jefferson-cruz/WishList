using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Repositories.Context;

namespace WishList.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WishListContext context;

        public ProductRepository(WishListContext context)
        {
            this.context = context;
        }

        public void Add(Product user)
        {
             this.context.Products.AddAsync(user);
        }

        public void Remove(Product product)
        {
            this.context.Products.Remove(product);
        }
    }
}
