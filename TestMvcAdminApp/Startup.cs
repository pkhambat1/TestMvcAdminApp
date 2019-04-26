using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCoreAdminApp.Data;
using MvcCoreAdminApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcCoreAdminApp.Repositories;

namespace MvcCoreAdminApp {
    public class Startup {

        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString { get; private set; }

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

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];

            //CreateUserRoles(services).Wait();
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
        public async Task CreateUserRoles(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            IDictionary<string, string> roleNamesRoleDescriptions = new Dictionary<string, string>();

            //Adding Addmin Role  
            roleNamesRoleDescriptions.Add(new KeyValuePair<string, string>("User", "Just a user"));
            roleNamesRoleDescriptions.Add(new KeyValuePair<string, string>("Admin", "Can View users and assign Roles"));
            roleNamesRoleDescriptions.Add(new KeyValuePair<string, string>("HRManager", "Can view users"));

            foreach (KeyValuePair<string, string> item in roleNamesRoleDescriptions) {
                var roleExists = await roleManager.RoleExistsAsync(item.Key);
                if (!roleExists) {
                    // Create Role Object
                    var role = new Role { Name = item.Key, Description = item.Value };
                    // Add Roles entry to Roles db table
                    var roleID = AdminRepository.CreateRole(role);

                    // Create ApplicationRole object with RoleID from Roles table
                    var applicationRole = new ApplicationRole(item.Key, roleID);
                    // Add AspNetRoles entry to AspNetRoles Table
                    var result = await roleManager.CreateAsync(applicationRole);

                }
            }
        }

        public async Task AssignSuperAdmin(IServiceProvider services) {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //Assign Admin role to the main User here we have given our newly registered login id for Admin management  
            ApplicationUser user = await userManager.FindByEmailAsync("pezanne2@email.com");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
