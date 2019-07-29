using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Services.Models.Common;
using WishList.Services.Models.User;

namespace WishList.Repositories.ReadOnly.Interfaces
{
    public interface IUserQueryRepository
    {
        Task<IEnumerable<UserModel>> GetAll(PaginationModel paginationModel);
        Task<UserModel> GetById(int id);
        Task<bool> UserExists(int id);
        Task<bool> UserExists(string email);
    }
}