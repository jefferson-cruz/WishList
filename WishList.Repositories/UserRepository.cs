using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Repositories.Context;

namespace WishList.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WishListContext context;

        public UserRepository(WishListContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            this.context.Users.Add(user);
        }

        public void Remove(User user)
        {
            this.context.Users.Remove(user);
        }
    }
}
