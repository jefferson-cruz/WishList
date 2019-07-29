using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WishList.Repositories.Seed;

namespace WishList.API.CustomersAndProducts.Extensions
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// Popula os bancos de dados da aplicação com dados fictícios
        /// </summary>
        /// <param name="app"></param>
        public static void DatabaseSeed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseSeed = new DatabaseSeed(serviceScope.ServiceProvider);

                databaseSeed.Seed();
            }
        }

        /// <summary>
        /// Registra a biblioteca AspNetRateLimit para controlar a quantidade de requisições da API.
        /// As configurações sobre a quantidade de requisições da API estão no arquivo appsettings.json 
        /// na seção "IpRateLimiting"
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRateLimit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
