namespace TestMvcAdminApp.Models {
    public class EditRolesOfUser {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
    }
}
