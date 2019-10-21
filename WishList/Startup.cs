using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WishList.API.CustomersAndProducts.Extensions;
using WishList.Services.IoC;
using WishList.Repositories.IoC;
using WishList.Mapping.Models;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using WishList.Shared.Repositories;
using System;

namespace WishList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(setup =>
            {
                setup.Configuration = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            });

            services.AddOptions();

            services.AddRateLimit(this.Configuration);

            RegisterDependencies(services);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.DatabaseSeed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIpRateLimiting();

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected fault happend. Try again later");
                });
            });

            app.UseResponseCompression();

            AutoMapper.Mapper.Initialize(cfg => MappingModels.Initialize(cfg));

            app.UseHttpsRedirection();

            app.UseMvc();
        }
            

        private void RegisterDependencies(IServiceCollection services)
        {
            var connectionStringMenager = new ConnectionStringManager(this.Configuration.GetSection("ConnectionStrings"));

            services.AddRepositories(connectionStringMenager);
            services.AddServices();
        }
    }
}
