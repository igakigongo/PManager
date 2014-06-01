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

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "* required")]
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool IsCompleted { get; set; }

        public Actual Actual { get; set; }

        public Estimated Estimated { get; set; }

        //Navigation Property
        public Project Project { get; set; }

        //This means that this task may be executed by one or more users:::more like a sub team
        public User User { get; set; }
    }
}
