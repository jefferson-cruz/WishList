using System.Threading.Tasks;
using WishList.Models.Product;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IProductService 
    {
        Task<Result<ProductModel>> Create(ProductCreationModel productModel);
    }
}
