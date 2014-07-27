using System.Web.Mvc;
using System.Web.Security;
using PManager.Domain.Abstract;
using PManager.Domain.Entities;
using PManager.WebUI.Filters;
using WebMatrix.WebData;

namespace PManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IDataTransferObject _dto;

        public HomeController(IDataTransferObject dtoParam)
        {
            _dto = dtoParam;
        }

        [InitializeSimpleMembership]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [InitializeSimpleMembership]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid || !WebSecurity.Login(model.UserName, model.Password, model.RememberMe)) return View(model);
            var userrole = Roles.GetRolesForUser()[0];
            switch (userrole)
            {
                case "Admin":
                    return RedirectToAction("AdminIndex");
                        
                case "Manager":
                    return RedirectToAction("Index");

                case "Normal":
                    return RedirectToAction("NormalIndex");
            }
            return View(model);
        }

        //
        // GET: /Home/
        [Authorize(Roles="Manager")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [Authorize(Roles = "Normal")]
        public ActionResult NormalIndex()
        {
            return View();
        }
	}
}