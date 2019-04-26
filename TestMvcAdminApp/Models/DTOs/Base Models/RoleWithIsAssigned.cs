using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RoleWithIsAssigned : Role {
        public bool IsAssigned { get; set; }
    }
}
