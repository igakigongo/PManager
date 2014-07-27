using System.Collections.Generic;

namespace PManager.Domain.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> UserIds { get; set; } 
    }
}
