using Microsoft.EntityFrameworkCore;
using WishList.Domain.Entities;
using WishList.Repositories.Mapping;

namespace WishList.Repositories.Context
{
    public class WishListContext : DbContext
    {
        public WishListContext(DbContextOptions<WishListContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wish> Wishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new WishMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}