using System.Threading.Tasks;
using WishList.Services.Models.User;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IUserService 
    {
        Task<Result<UserModel>> Create(UserCreationModel userModel);
    }
}
