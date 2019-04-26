using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAdminApp.Models {

    public class ListUsers {

        public List<ApplicationUser> Users { get; set; }
        public ListUsers() {
            Users = new List<ApplicationUser>();
        }
        public int Count { get; set; }
    }


    public class ApplicationUser : IdentityUser {
        // Foreign key from UserDetails
        public int UserID { get; set; }
        public override string Email { get; set; }
        public override string UserName { get; set; }
    }
}
