using System.Collections.Generic;

namespace TestMvcAdminApp.Models {
    public class UserDetailsWithRoles : UserDetails {
        public List<Role> Roles { get; set; }
    }
}
