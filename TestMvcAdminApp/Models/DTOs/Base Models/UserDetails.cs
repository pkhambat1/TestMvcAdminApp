using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class UserDetails {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}
