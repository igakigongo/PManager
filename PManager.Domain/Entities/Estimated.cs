using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Estimated
    {
        [Required(ErrorMessage = "* required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "* required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "* required")]
        public Decimal Budget { get; set; }
    }
}
