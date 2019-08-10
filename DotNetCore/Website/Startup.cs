using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shahnameh.Models;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ShahnamehBekhanim
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<DataModel>(options => {
                if (this.Configuration.GetValue<bool>("UseMySql")) {
                    options.UseMySql(this.Configuration.GetConnectionString("Database"), mySqlOptions => {
                        mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql);
                    });
                } else if (this.Configuration.GetValue<bool>("UseSqlite")) {
                    options.UseSqlite(this.Configuration.GetConnectionString("Database"));
                }
                else {
                    options.UseSqlServer(this.Configuration.GetConnectionString("Database"));
                }
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles(new StaticFileOptions { 
                ServeUnknownFileTypes = true, 
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Static")),
                RequestPath = "/Static",
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
