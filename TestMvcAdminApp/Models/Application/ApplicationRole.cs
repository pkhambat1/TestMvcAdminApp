using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {

    public class ApplicationRole : IdentityRole {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name, int roleID) : base(name) {
            RoleID = roleID;
        }
        public int RoleID { get; set; }
    }

}
