using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class PermissionsWithRights : Permission {
        public List<Right> Rights { get; set; }
    }
}
