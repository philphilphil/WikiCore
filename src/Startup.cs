using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WikiCore.Configuration;
using WikiCore.DB;

namespace WikiCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Initialise di for database
            var connectionString = "Filename=./" + Configuration["ConnectionStrings:SqliteFileName"];
            services.AddDbContext<WikiContext>(options => options.UseSqlite(connectionString));
            DbInitializer.InitializeDb(connectionString);

            services.AddScoped<IDBService, DBService>();

            services.Configure<ApplicationConfigurations>(Configuration.GetSection("ApplicationConfigurations"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<WikiContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // Cookie settings
                    options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                    options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                    options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                    // User settings
                    options.User.RequireUniqueEmail = false;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddConsole(LogLevel.Debug);
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentity();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "add",
                    template: "Add",
                    defaults: new { controller = "Edit", action = "Add" }
                    );

                routes.MapRoute(
                        "Edit",
                        "Edit/{id}",
                        new { controller = "Edit", action = "Index" },
                        new { id = @"\d*" }
                    );

                routes.MapRoute(
                        "Tag",
                        "Tag/{name}",
                        new { controller = "Home", action = "Tag" },
                        new { name = @"\w+" }
                    );

                routes.MapRoute(
                        "Page",
                        "Page/{id}",
                        new { controller = "Home", action = "Index" },
                        new { id = @"\d*" }
                    );

                routes.MapRoute(
                       "Cloud",
                       "Cloud",
                       new { controller = "Home", action = "Cloud" }
                   );

                routes.MapRoute(
                        "Login",
                        "Login",
                        new { controller = "Account", action = "Login" }
                    );

                routes.MapRoute(
                        "Logout",
                        "Logout",
                        new { controller = "Account", action = "Logout" }
                    );

                routes.MapRoute(
                        "Register",
                        "Register",
                        new { controller = "Account", action = "Register" }
                    );
            });
        }
    }
}
