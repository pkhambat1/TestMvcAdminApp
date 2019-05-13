namespace TestMvcAdminApp.Models {
    public class UserDetailsWithRoleDTO : UserDetails {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}