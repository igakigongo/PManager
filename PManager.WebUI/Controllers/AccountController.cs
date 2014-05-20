using PManager.Domain.Entities;
using PManager.WebUI.Filters;
using PManager.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebMatrix.WebData;

namespace PManager.WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        // private IAuthProvider authProvider;
        // Will inject data later on

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration=Int32.MaxValue, Location=OutputCacheLocation.ServerAndClient)]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(Membership.ValidateUser(model.UserName, model.Password))
                {
                    //WebSecurity - check user state and redirect to the necessary role
                    WebSecurity.Login(userName: model.UserName, password: model.Password, persistCookie: model.RememberMe);
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    String[] UserRoles = Roles.GetRolesForUser(model.UserName);
                    if (UserRoles[0].ToString() == "Manager")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
	}
}