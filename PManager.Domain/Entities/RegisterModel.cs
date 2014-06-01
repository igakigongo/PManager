using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "* Required")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Compare("Password", ErrorMessage = "* Passwords do not match.")]
        public string ComparePassword { get; set; }

        public string Role { get; set; }
    }
}
