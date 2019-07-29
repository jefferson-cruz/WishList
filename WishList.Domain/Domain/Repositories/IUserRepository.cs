using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WishList.Domain.Entities;

namespace WishList.Domain.Repositories
{
    public interface IUserRepository
    {
        void Add(Entities.User user);
        void Remove(User user);
    }
}
