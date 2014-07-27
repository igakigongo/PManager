using PManager.Domain.Entities;
using PManager.WebUI.Filters;
using System;
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
                    WebSecurity.Login(model.UserName, model.Password, model.RememberMe);
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    String[] userRoles = Roles.GetRolesForUser(model.UserName);
                    String userrole = userRoles[0];
                    if (userrole == "Manager")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userrole == "Admin")
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                    else if (userrole == "Normal")
                    {
                        return RedirectToAction("NormalIndex", "Home");
                    }
                    return RedirectToAction("Login");
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

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login");
        }
	}
}