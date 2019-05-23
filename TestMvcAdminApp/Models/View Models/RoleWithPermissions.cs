using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RoleWithPermissions : Role {
        public List<Permission> Permissions { get; set; }
        public int UsersCount { get; set; }
    }
}
