using Microsoft.Extensions.DependencyInjection;
using WishList.Services.Interfaces;

namespace WishList.Services.IoC
{
    public static class Register
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IWishService, WishService>();
            services.AddTransient(typeof(IIndexService<>), typeof(IndexService<>));

            return services;
        }
    }
}
