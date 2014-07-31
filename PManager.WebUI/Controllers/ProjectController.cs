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
using System.Drawing;
using PManager.Domain.Abstract;

namespace PManager.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IDataTransferObject _dto;
        private readonly EFDbContext _context = new EFDbContext();

        public ProjectController(IDataTransferObject dtoParam)
        {
            _dto = dtoParam;
        }

        // GET: /Project/
        [HttpGet]
        public ActionResult Index(string filter)
        {
            return filter == null ? View(_context.Projects.ToList()) : View(_context.Projects.Where(p => p.IsClosed == false).OrderBy(p => p.ProjectCode));
        }

        // GET: /Project/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _context.Projects
                .Include(p => p.ProjectTasks)
                .OrderByDescending(p => p.Estimated.StartDate).SingleOrDefault(p => p.Id == id);
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
            return View();
        }

        // POST: /Project/Create
        [HttpPost]
        public JsonResult Create(Project project)
        {
            if (!ModelState.IsValid) return Json(_dto, JsonRequestBehavior.AllowGet);
            try
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                _dto.message = "success";
            }
            catch (DbUpdateException ex)
            {
                _dto.message = "Error: " + ex.Message;
            }
            return Json(_dto, JsonRequestBehavior.AllowGet);
        }

        // GET: /Project/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (!ModelState.IsValid) return View(project);
            _context.Projects.Attach(project);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Project/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult GetProjects()
        {
            return Json(_context.Projects.Where(x=>x.IsClosed==false), JsonRequestBehavior.AllowGet);
        }

        #region HighCharts

        #region 1.0 Recent Top Ten Project Costing
        public JsonResult TopTenProjectsCosting()
        {
            var projects = _context.Projects.Where(p => p.IsClosed == false).OrderByDescending(p => p.Estimated.StartDate).Take(10);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}
