using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RolesWithPermissions : Role {
        public List<Permission> Permissions { get; set; }
    }
}
