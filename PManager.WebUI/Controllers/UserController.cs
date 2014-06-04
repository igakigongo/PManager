using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PManager.Domain.Entities;
using PManager.Domain.Concrete;
using System.Data.Entity.Infrastructure;
using PManager.Domain.Abstract;
using PManager.WebUI.Filters;
using System.Web.Security;
using WebMatrix.WebData;

namespace PManager.WebUI.Controllers
{
    public class UserController : Controller
    {
        
        private UnitOfWork _unitOfWork = new UnitOfWork();
        
        // GET: /User/
        public ActionResult Index()
        {
            IQueryable<User> _users = _unitOfWork.UserRepository.Get().AsQueryable();
            return View(_users);
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        [InitializeSimpleMembership]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [InitializeSimpleMembership]
        public ViewResult Create(RegisterModel model)
        {
            String message = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser _user = Membership.GetUser(model.UserName);
                    if (_user == null)
                    {
                        WebSecurity.CreateUserAndAccount(userName: model.UserName, password: model.Password, propertyValues: null, requireConfirmationToken: false);
                        Roles.AddUserToRole(username: model.UserName, roleName: model.Role);
                        Domain.Entities.User _systemUser = new Domain.Entities.User
                        {
                            Firstname = model.Firstname,
                            Lastname = model.Lastname,
                            Middlename = model.Middlename,
                            Id =  WebSecurity.GetUserId(model.UserName)
                        };
                        _unitOfWork.UserRepository.Add(_systemUser);
                        _unitOfWork.Save();
                        message = "success";
                    }
                    else
                    {
                        message = String.Format("The username {0} is already taken, please choose a different one.");
                    }
                }
                catch (DbUpdateException)
                {
                    message = "Error, Please try again and if the problem persists, contact your system vendor.";
                }
            }
            ViewBag.message = message;
            return View(model);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(_unitOfWork.UserProfiles, "UserId", "UserName", user.Id);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.UserRepository.Update(user); 
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(_unitOfWork.UserProfiles, "UserId", "UserName", user.Id);
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = _unitOfWork.UserRepository.Find(id);
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Highcharts Data and Json Requests
        
        #endregion
    }
}
