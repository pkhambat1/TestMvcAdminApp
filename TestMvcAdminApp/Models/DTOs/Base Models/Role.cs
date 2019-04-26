using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAdminApp.Models {
    public class Role {
        public int ID { get; set; }
        [Required, Display(Name = "Role")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
