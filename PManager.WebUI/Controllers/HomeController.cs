using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize(Roles="Manager")]
        public ActionResult Index()
        {
            return View();
        }
	}
}