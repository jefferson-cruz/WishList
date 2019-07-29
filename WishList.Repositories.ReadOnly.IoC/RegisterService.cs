using Microsoft.Extensions.DependencyInjection;
using WishList.Repositories.ReadOnly.Context;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Shared.Repositories;

namespace WishList.Repositories.ReadOnly.IoC
{
    public static class RegisterService
    {
        public static IServiceCollection AddRepositoriesReadOnly(this IServiceCollection services, ConnectionStringManager connectionString)
        {
            var connectionStringElasticSearch = connectionString.ConnectionStrings[ConnectionStrings.WishListElasticSearch];

            var elasticSearch = new QueryContext(connectionStringElasticSearch);

            services.AddSingleton(elasticSearch);

            services.AddScoped<IUserQueryRepository, UserQueryRepository>(factory => new UserQueryRepository(elasticSearch));
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>(factory => new ProductQueryRepository(elasticSearch));
            services.AddScoped<IWishQueryRepository, WishQueryRepository>(factory => new WishQueryRepository(elasticSearch));

            return services;
        }
    }
}
