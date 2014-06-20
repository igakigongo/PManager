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

namespace PManager.WebUI.Controllers
{
    public class ProjectTaskController : Controller
    {
        private IDataTransferObject _dto;
        private EFDbContext _context = new EFDbContext();

        public ProjectTaskController(IDataTransferObject _dtoParam)
        {
            _dto = _dtoParam;
        }

        // GET: /ProjectTask/
        public ActionResult Index()
        {
            IQueryable<ProjectTask> _projecttasks = _context.ProjectTasks.Include(t => t.Team).Include(t => t.Project);
            return View(_projecttasks);
        }

        // GET: /ProjectTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask _projecttask = _context.ProjectTasks.Include(t => t.Project).Include(t => t.Team).Where(t => t.Id == id).SingleOrDefault();
            if (_projecttask == null)
            {
                return HttpNotFound();
            }
            return View(_projecttask);
        }

        // POST: /ProjectTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(ProjectTask projecttask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProjectTasks.Add(projecttask);
                    _context.SaveChanges();
                    _dto.message = "success";
                }
                catch (DbUpdateException)
                {
                    _dto.message = "Error, Please try again and if the problem persists contact your system vendor.";
                }
            }
            return Json(_dto, JsonRequestBehavior.AllowGet);
        }

        // GET: /ProjectTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projecttask = _context.ProjectTasks.Include(t => t.Project).Include(t => t.Team).SingleOrDefault(t => t.Id == id);
            if (projecttask == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = new SelectList(_context.Users, "Id", "Fullname");
            //ViewBag.ProjectId = new SelectList(_unitOfWork.Projects, "Id", "ProjectCode", projecttask.ProjectId);
            //ViewBag.UserId = new SelectList(_unitOfWork.UserRepository.Get(), "Id", "Fullname", projecttask.UserId);
            return View(projecttask);
        }

        // POST: /ProjectTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ProjectTaskViewModel projecttaskvm)
        {
            if (ModelState.IsValid)
            {
                // pick all team members that are new and complete the projecttaskvm fully
                //projecttaskvm.AssociatedMemberIds.ForEach(t => projecttaskvm.Task.Team.Add(_context.Users.Find(t)));


                var existingtask = _context.ProjectTasks.Include(t => t.Team);

                //_context.Entry(projecttaskvm.Task).State = EntityState.Modified;
                //_context.SaveChanges();
                //foreach (int id in projecttaskvm.AssociatedMemberIds)
                //{
                //    projecttaskvm.Task.Team.Add(_context.Users.Find(id));

                //}
                _context.SaveChanges();
                _dto.message = "success";
                if (Request.IsAjaxRequest())
                {
                    return Json(_dto, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Details", "Project", new { id = projecttaskvm.Task.ProjectId });
                }
            }
            return View(projecttaskvm.Task);
        }

        // GET: /ProjectTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projecttask = _context.ProjectTasks.Find(id);
            if (projecttask == null)
            {
                return HttpNotFound();
            }
            return View(projecttask);
        }

        // POST: /ProjectTask/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projecttask = _context.ProjectTasks.Find(id);
            _context.ProjectTasks.Remove(entity: projecttask);
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

        #region HighCharts 

        #region 1.0 Requesting All Tasks For A Given Project
        public JsonResult GetAllTasks(int projectId)
        {
            var _tasks = _context
                .ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.Estimated.StartDate);
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}
