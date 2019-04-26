using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RolesWithPermissionsDTO : Role {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
    }
}
