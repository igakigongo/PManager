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
        
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly EFDbContext _db;

        public UserController()
        {
            _db = new EFDbContext();
        }

        // GET: /User/
        public ActionResult Index()
        {
            var users = _unitOfWork.UserRepository.Get().ToList();
            users.ForEach(user => user.UserProfile = _unitOfWork.UserProfileRepository.Find(user.Id));
            return View(users);
        }

        public ActionResult GetAllUsers()
        {
            return Json(_unitOfWork.UserRepository.Get().ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Include(u => u.UserProfile).SingleOrDefault(u => u.Id == id);
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
        public ActionResult Create(RegisterModel model)
        {
            var message = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Membership.GetUser(model.UserName);
                    if (user == null)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                        Roles.AddUserToRole(model.UserName, model.Role);
                        var systemUser = new User
                        {
                            EmailAddress = model.EmailAddress,
                            Firstname = model.Firstname,
                            Id = WebSecurity.GetUserId(model.UserName),
                            Lastname = model.Lastname,
                            Middlename = model.Middlename,
                            PhoneContact = model.PhoneContact
                        };
                        _unitOfWork.UserRepository.Add(systemUser);
                        _unitOfWork.Save();
                        ViewBag.message = "success"; 
                        return RedirectToAction("Index");
                    }
                    message = String.Format("The username {0} is already taken, please choose a different one.", model.UserName);
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
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
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
            var user = _unitOfWork.UserRepository.Find(id);
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
            var user = _unitOfWork.UserRepository.Find(id);
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
