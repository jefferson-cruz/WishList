using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Models.Wish;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IWishService 
    {
        Task<Result<WishModel>> Save(int userId, IEnumerable<WishCreationModel> wishCreationModel);
        Task<Result> Remove(int userId, int productId);
    }
}
