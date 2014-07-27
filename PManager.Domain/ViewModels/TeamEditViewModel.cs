using System.Web.Mvc;

namespace PManager.Domain.ViewModels
{
    public class TeamEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SelectList UserList { get; set; }
    }
}
