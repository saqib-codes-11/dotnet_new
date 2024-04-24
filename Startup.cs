using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigProject.Data;
using BigProject.Data.Entities;
using BigProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BigProject
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
            services.AddDbContext<BigProjectContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("BigProjectConnectionString"));
            });
            /* transient no data just does soemthing
             * addscoped data havy
             * singleton lifetime object
             */
            services.AddTransient<IMailService, NullMailService>();
            services.AddTransient<BigProjectContextSeeder>();
            // repositor implementations
            // services.AddScoped<Interface, Implementation>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {  
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // it looks onyl inside of wwwroot file css font index.html all comes into this file
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment()) {
                using (var scope = app.ApplicationServices.CreateScope()) {
                    var seeder = scope.ServiceProvider.GetService<BigProjectContextSeeder>();
                    seeder.Seed();
                }
            }
        }
    }
}
