using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net.core.mvc.demo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace asp.net.core.mvc.demo
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore();
            services.AddControllersWithViews();
            services.AddSingleton<Features>(x => new Features
            {
                EnableDeveloperExceptions = configuration.GetValue<bool>("Features:EnableDeveloperExceptions")
            });
            services.AddDbContext<PlayerDataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PlayerDataContext"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Features features)
        {
            if (features.EnableDeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
