using System.Threading.Tasks;
using WishList.Services.Models.User;

namespace WishList.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserModel> Create(UserCreationModel userModel);
    }
}
