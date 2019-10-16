using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Models {
    public class Permission {
        public int ID { get; set; }
        [Required, Display(Name = "Permission")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
