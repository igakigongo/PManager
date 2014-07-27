using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage="* Invalid Email.")]
        [RegularExpression(pattern: ".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage="* Required.")]
        public string PhoneContact { get; set; }

        public string Role { get; set; }
    }
}
