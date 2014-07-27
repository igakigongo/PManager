using System;

namespace PManager.Domain.ViewModels
{
    public class ProjectTaskViewModel
    {
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public Decimal Budget { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
