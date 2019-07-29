using WishList.Domain.Repositories;
using WishList.Repositories.Context;

namespace WishList.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WishListContext context;

        public UnitOfWork(WishListContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
