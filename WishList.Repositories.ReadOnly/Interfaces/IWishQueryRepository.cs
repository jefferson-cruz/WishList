using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Models.Product;
using WishList.Models.Wish;
using WishList.Services.Models.Common;

namespace WishList.Repositories.ReadOnly.Interfaces
{
    public interface IWishQueryRepository
    {
        Task<IEnumerable<ProductModel>> GetAll(int userId, PaginationModel paginationModel);
        //Task<bool> Exists(int userId, int productId);
        //Task<bool> Exists(int userId, IEnumerable<int> productsIds);
        Task<WishModel> GetByUser(int userId);
    }
}
