using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class EditRightsOfPermission {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
    }
}
