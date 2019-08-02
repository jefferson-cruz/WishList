using System.Threading.Tasks;
using WishList.Services.Models.User;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<IResultBase> Create(UserCreationModel userModel);
    }
}
