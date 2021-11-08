using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.Repositories;
using CulinaryApi.Infrastructure.Services;
using CulinaryApi.Infrastructure.Services.Cuisines;
using CulinaryApi.Infrastructure.Services.Difficulties;
using CulinaryApi.Infrastructure.Services.meals;
using CulinaryApi.Infrastructure.Services.Recipes;
using CulinaryApi.Infrastructure.Services.Times;
using CulinaryApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi
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

            services.AddControllers();
            services.AddDbContext<CulinaryDbContext>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICuisineRepository, CuisineRepository>();
            services.AddScoped<ICuisineService, CuisineService>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<ITimeRepository, TimeRepository>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IDifficultyRepository, DifficultyRepository>();
            services.AddScoped<IDifficultyService, DifficultyService>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ErrorHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
