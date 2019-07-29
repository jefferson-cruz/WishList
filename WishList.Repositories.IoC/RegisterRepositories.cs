﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WishList.Domain.Repositories;
using WishList.Repositories.Context;
using WishList.Repositories.Index;
using WishList.Repositories.Index.Context;
using WishList.Repositories.Index.Interfaces;
using WishList.Repositories.ReadOnly;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Shared.Repositories;

namespace WishList.Repositories.IoC
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, ConnectionStringManager connectionStringManager)
        {
            #region Contexts
            services.AddEntityFrameworkSqlServer().AddDbContext<WishListContext>(options =>
            {
                options.UseSqlServer(connectionStringManager.ConnectionStrings[ConnectionStrings.WishList]);

            }, ServiceLifetime.Scoped);

            services.AddSingleton(new IndexContext(connectionStringManager.ConnectionStrings[ConnectionStrings.WishListIndex]));
            #endregion

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IWishRepository, WishRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(factory =>
            {
                return new UnitOfWork(factory.GetService<WishListContext>());
            });

            services.AddTransient(typeof(IIndexRepository<>), typeof(IndexRepository<>));
            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();
            services.AddTransient<IWishQueryRepository, WishQueryRepository>();

            return services;
        }
    }
}
