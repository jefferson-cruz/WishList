using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Models.Product;
using WishList.Services.Models.Common;

namespace WishList.Repositories.ReadOnly.Interfaces
{
    public interface IProductQueryRepository
    {
        Task<IEnumerable<ProductModel>> GetAll(PaginationModel paginationModel);
        Task<ProductModel> GetById(int id);
        Task<bool> ProductExists(int id);
        Task<bool> ProductExists(string nameOfProduct);
    }
}