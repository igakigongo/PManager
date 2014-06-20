using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class ProjectTaskViewModel
    {
        public ProjectTask Task { get; set; }

        public List<int> AssociatedMemberIds { get; set; }
    }
}
