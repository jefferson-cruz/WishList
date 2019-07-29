using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Models.Wish;

namespace WishList.Services.Interfaces
{
    public interface IWishService : IBaseService
    {
        Task Save(int userId, IEnumerable<WishCreationModel> wishCreationModel);
        Task Remove(int userId, int productId);
    }
}
