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
        private IDataTransferObject dto;
        private EFDbContext context = new EFDbContext();

        public ProjectController(IDataTransferObject dtoParam)
        {
            this.dto = dtoParam;
        }

        // GET: /Project/
        [HttpGet]
        public ActionResult Index(string filter)
        {

            if (filter == null)
            {
                return View(context.Projects.ToList());
            }
            else
            {
                return View(context.Projects.Where(p => p.IsClosed == false).OrderBy(p => p.ProjectCode));
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
            var project = context.Projects
                                     .Include(p => p.ProjectTasks)
                                     .OrderByDescending(p => p.Estimated.StartDate)
                                     .Where(p => p.Id == id)
                                     .SingleOrDefault();
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Projects.Add(project);
                    context.SaveChanges();
                    dto.message = "success";
                    dto.id = context.Projects.LastOrDefault().Id;
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
            var project = context.Projects.Find(id);
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
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                context.Projects.Attach(project);
                context.Entry(project).State = EntityState.Modified;
                context.SaveChanges();
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
            var project = context.Projects.Find(id);
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
            Project project = context.Projects.Find(id);
            context.Projects.Remove(project);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }


        #region HighCharts

        #region 1.0 Recent Top Ten Project Costing
        public JsonResult TopTenProjectsCosting()
        {
            var projects = context.Projects.Where(p => p.IsClosed == false).OrderByDescending(p => p.Estimated.StartDate).Take(10);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Reports
        //public JsonResult OnGoingProjects()
        //{
        //    var projects = _unitOfWork.ProjectRepository.Get(filter: t => t.IsClosed == false);
        //    projects.ForEach(
        //    return null;                           
        //}
        #endregion

        #endregion
    }
}
