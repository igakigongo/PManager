using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Team
    {
        public Team()
        {
            this.Status = Status.Active;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ProjectTask> Tasks { get; set; }

        public List<User> Users { get; set; }

        public Status Status { get; set; }
    }
}
