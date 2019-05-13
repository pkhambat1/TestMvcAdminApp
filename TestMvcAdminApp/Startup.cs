using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMvcAdminApp.Data;
using TestMvcAdminApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestMvcAdminApp.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TestMvcAdminApp {
    public class Startup {

        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString { get; private set; }
        private readonly Action<IRouteBuilder> GetRoutes = routes => {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        };

        public Startup(IHostingEnvironment env) {
            Configuration = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
                                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                                                    .AddEntityFrameworkStores<ApplicationDbContext>()
                                                    .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, ILoggerFactory loggerFactory, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment() || env.IsEnvironment("Azure")) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(GetRoutes);
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];

            // setup routes
            app.UseGetRoutesMiddleware(GetRoutes);

            app.Run(async context => {
                await context.Response.WriteAsync("Page Unavailable");
            });

            CreateAdminRole(services).Wait();
            AssignSuperAdmin(services).Wait();
        }

        public static string GetConnectionString() {
            return ConnectionString;
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                var con = GetConnectionString();
                optionsBuilder.UseSqlServer(con);
            }
        }

        // If all works out, this method will be obviated and not required
        public async Task CreateAdminRole(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            IDictionary<string, string> roleNamesRoleDescriptions = new Dictionary<string, string>();

            // Super Admin
            var superAdminRoleName = "IT Admin";

            //Adding Admin Role  
            roleNamesRoleDescriptions.Add(new KeyValuePair<string, string>(superAdminRoleName, ""));

            foreach (var roleItem in roleNamesRoleDescriptions) {
                var roleExists = await roleManager.RoleExistsAsync(roleItem.Key);
                if (!roleExists) {
                    // Create ApplicationRole object with RoleID from Roles table
                    var applicationRole = new ApplicationRole(roleItem.Key, 0);
                    // Add AspNetRoles entry to AspNetRoles Table
                    await roleManager.CreateAsync(applicationRole);
                    // Create Role-Claim mapping with default value "false"  
                    var claims = AdminRepository.GetAllRights();
                    // Assign all Claims to IT Admin hidden role
                    foreach (var claimItem in claims) {
                        await roleManager.AddClaimAsync(applicationRole, new Claim(claimItem.Name, "True"));
                    }
                }
            }
        }

        public async Task AssignSuperAdmin(IServiceProvider services) {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //Assign Admin role to the main User here we have given our newly registered login id for Admin management  
            ApplicationUser user = await userManager.FindByEmailAsync("pezanne2@email.com");
                await userManager.AddToRoleAsync(user, "IT Admin");
        }
    }
}
