using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RolesForUserDTO {
        public RolesForUserDTO() {
            RolesWithIsAssigned = new List<RoleWithIsAssigned>();
        }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RoleWithIsAssigned> RolesWithIsAssigned { get; set; }
    }
}
