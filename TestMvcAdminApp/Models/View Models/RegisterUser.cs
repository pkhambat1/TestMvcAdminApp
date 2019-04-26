using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class RegisterUser {

        [Required]
        [MaxLength(256)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(256),
        Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required, Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required, Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
