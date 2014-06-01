using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class User
    {
        [Key]
        [ForeignKey("UserProfile")]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Lastname { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return Middlename == null ?
                    String.Format("{0} {1}", Lastname, Firstname) :
                    String.Format("{0} {1} {2}", Lastname, Middlename, Firstname);
            }
        }

        public UserProfile UserProfile { get; set; }
    }
}
