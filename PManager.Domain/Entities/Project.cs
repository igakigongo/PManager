using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Project
    {
        //
        // Configuration for Complex Types Actual and Estimated

        public Project()
        {
            Actual = new Actual();
            Estimated = new Estimated();
            ProjectTasks = new List<ProjectTask>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //
        // BOU Project Specific code

        [Required(ErrorMessage = "* required")]
        public string ProjectCode { get; set; }

        //
        // The Project Name

        [Required(ErrorMessage = "* required")]
        public string Name { get; set; }


        public bool IsClosed { get; set; }

        //
        // Actual values of project properties:::time(schedule) and costs

        public Actual Actual { get; set; }

        //
        // Estimate values of project properties::::time(schedule) and cost

        public Estimated Estimated { get; set; }

        // 
        // Description of the project
        [DataType(dataType: DataType.MultilineText)]
        public string Description { get; set; }

        //
        // The list of task for the project

        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
