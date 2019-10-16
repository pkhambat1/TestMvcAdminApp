using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class PermissionWithIsAssigned : Permission {
        public bool IsAssigned { get; set; }
    }
}
