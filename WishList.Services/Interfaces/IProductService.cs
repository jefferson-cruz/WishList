using System.Threading.Tasks;
using WishList.Models.Product;

namespace WishList.Services.Interfaces
{
    public interface IProductService : IBaseService 
    {
        Task<ProductModel> Create(ProductCreationModel productModel);
    }
}
