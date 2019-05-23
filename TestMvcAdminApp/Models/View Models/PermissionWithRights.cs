using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class PermissionWithRights : Permission {
        public List<Right> Rights { get; set; }
        public int RolesCount { get; set; }
    }
}
