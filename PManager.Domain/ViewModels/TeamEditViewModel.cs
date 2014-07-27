using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PManager.Domain.Entities;

namespace PManager.Domain.ViewModels
{
    public class TeamEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SelectList UserList { get; set; }
    }
}
