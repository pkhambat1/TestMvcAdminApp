using TestMvcAdminApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestMvcAdminApp.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<UserDetails> UserDetails { get; set; }

        //public DbSet<Role> Role { get; set; }

        //public DbSet<Right> Right { get; set; }

        //public DbSet<Permission> Permission { get; set; }

    }
}
