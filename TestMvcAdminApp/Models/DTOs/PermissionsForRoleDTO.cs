using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class PermissionsForRoleDTO {
        public PermissionsForRoleDTO() {
            PermissionsWithIsAssigned = new List<PermissionWithIsAssigned>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<PermissionWithIsAssigned> PermissionsWithIsAssigned { get; set; }
    }
}
