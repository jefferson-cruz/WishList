using System.Threading.Tasks;
using WishList.Domain.Entities;

namespace WishList.Domain.Repositories
{
    public interface IProductRepository
    {
        void Add(Entities.Product user);
        void Remove(Product product);
    }
}
