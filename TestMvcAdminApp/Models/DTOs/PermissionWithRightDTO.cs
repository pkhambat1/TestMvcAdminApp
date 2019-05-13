using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class PermissionWithRightDTO : Permission {
        public int RightID { get; set; }
        public string RightName { get; set; }
        public string RightDescription { get; set; }
    }
}
