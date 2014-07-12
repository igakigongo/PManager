using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIds { get; set; } 
    }
}
