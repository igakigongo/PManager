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

namespace PManager.WebUI.Controllers
{
    public class UserController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: /User/
        public ActionResult Index()
        {
            var users = _unitOfWork.UserRepository.Get();
            return View(users.ToList());
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
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(_unitOfWork.UserProfiles, "UserId", "UserName");
            return View();
        }

        // POST: /User/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Firstname,Middlename,Lastname")] User user)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(_unitOfWork.UserProfiles, "UserId", "UserName", user.Id);
            return View(user);
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Firstname,Middlename,Lastname")] User user)
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
        [ValidateAntiForgeryToken]
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
    }
}
