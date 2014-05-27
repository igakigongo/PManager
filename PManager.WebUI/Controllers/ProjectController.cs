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

namespace PManager.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: /Project/
        [HttpGet]
        public ActionResult Index(string filter)
        {

            if (filter == null)
            {
                return View(_unitOfWork.ProjectRepository.Get());
            }
            else
            {
                return View(_unitOfWork.ProjectRepository.Get(filter: p => p.IsClosed == false, orderBy: null));
            }
        }

        // GET: /Project/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _unitOfWork.ProjectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            SelectList _userList = new SelectList(_unitOfWork.UserRepository.Get(), "Id", "FullName");
            ViewBag.UserList = _userList;
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(Project project)
        {
            // load the Json Data Transfer Object
            JsonDTO dto = new JsonDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProjectRepository.Add(project);
                    _unitOfWork.Save();;
                    dto.message = "success";
                    dto.id = _unitOfWork.ProjectRepository.Get().LastOrDefault().Id;
                }
                catch (DbUpdateException ex)
                {
                    dto.message = "Error: " + ex.Message;
                }
            }

            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        // GET: /Project/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _unitOfWork.ProjectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectCode,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProjectRepository.Update(project);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _unitOfWork.ProjectRepository.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = _unitOfWork.ProjectRepository.Find(id);
            _unitOfWork.ProjectRepository.Delete(project);
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
