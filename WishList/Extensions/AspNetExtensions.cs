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
        /// Populate databases with fictitious data
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
        /// Registers the AspNet RateLimit library to control the amount of API requests.
        /// The setting is in the appsettings.json file in the "IpRalteLimiting" section.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRateLimit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

            services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                // get the IpPolicyStore instance
                var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();
                
                // seed IP data from appsettings
                ipPolicyStore.SeedAsync().Wait();
            }
        }
    }
}
