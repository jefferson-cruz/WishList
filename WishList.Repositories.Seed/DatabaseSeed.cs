using System;
using WishList.Repositories.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WishList.Domain.Entities;
using WishList.Repositories.Index.Context;
using WishList.Models.Product;
using WishList.Services.Models.User;
using WishList.Models.Wish;
using System.Linq;

namespace WishList.Repositories.Seed
{
    public class DatabaseSeed
    {
        private readonly IServiceProvider serviceProvider;

        public DatabaseSeed(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public void Seed()
        {
            var wishListContext = serviceProvider.GetService<WishListContext>();

            wishListContext.Database.EnsureDeleted();
            wishListContext.Database.Migrate();

            wishListContext.Wishes.RemoveRange(wishListContext.Wishes);
            wishListContext.Users.RemoveRange(wishListContext.Users);
            wishListContext.Products.RemoveRange(wishListContext.Products);

            wishListContext.Users.AddRange(
                User.Create("Rodrigo Carvalho", "rodrigo@emailteste.com").Value,
                User.Create("Marcel Grilo", "marcel.grilo@emailteste.com").Value,
                User.Create("Alexandre Faria", "alexandre@emailteste.com").Value
            );

            wishListContext.Products.AddRange(
                Product.Create("Batedeira").Value,
                Product.Create("Vídeo Cassete").Value,
                Product.Create("Toca Fitas").Value
            );

            wishListContext.SaveChanges();

            SeedIndexerDatabase(wishListContext);
        }

        private void SeedIndexerDatabase(WishListContext wishListContext)
        {
            var indexContext = serviceProvider.GetService<IndexContext>();

            indexContext.DeleteIndex<ProductModel>();
            indexContext.DeleteIndex<UserModel>();
            indexContext.DeleteIndex<WishModel>();

            indexContext.ExecuteMappings();

            indexContext.BulkInsert(wishListContext.Users.Select(x => new UserModel { Id = x.Id, Email = x.Email, Name = x.Name }));
            indexContext.BulkInsert(wishListContext.Products.Select(x => new ProductModel { Id = x.Id, Name = x.Name }));
        }
    }

}
