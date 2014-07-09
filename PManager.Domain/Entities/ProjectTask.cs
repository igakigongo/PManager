using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class ProjectTask
    {
        //configuring for complexity: complex type
        public ProjectTask()
        {
            Actual = new Actual();
            Estimated = new Estimated();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "* required")]
        public string TaskName { get; set; }

        public bool IsCompleted { get; set; }

        public Actual Actual { get; set; }

        public Estimated Estimated { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
