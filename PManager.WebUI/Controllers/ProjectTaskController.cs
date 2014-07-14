using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PManager.Domain.Entities;
using PManager.Domain.Concrete;
using System.Data.Entity.Infrastructure;
using PManager.Domain.Abstract;
using ProjectTaskViewModel = PManager.Domain.ViewModels.ProjectTaskViewModel;

namespace PManager.WebUI.Controllers
{
    public class ProjectTaskController : Controller
    {
        private IDataTransferObject _dto;
        private EFDbContext _context = new EFDbContext();
        private GenericRepository<ProjectTask> projectTaskRepository;
        private UnitOfWork unitOfWork;
        public ProjectTaskController(IDataTransferObject _dtoParam)
        {
            projectTaskRepository = new GenericRepository<ProjectTask>(_context);
            unitOfWork = new UnitOfWork();
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

        public ActionResult Create()
        {
            return View();
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
            ViewBag.Team = new SelectList(_context.Teams, "Id", "Name");
            //ViewBag.ProjectId = new SelectList(_unitOfWork.Projects, "Id", "ProjectCode", projecttask.ProjectId);
            //ViewBag.UserId = new SelectList(_unitOfWork.UserRepository.Get(), "Id", "Fullname", projecttask.UserId);
            return View(projecttask);
        }

        // POST: /ProjectTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ProjectTask projecttaskvm)
        {
            if (ModelState.IsValid)
            {

                _context.ProjectTasks.AddOrUpdate(projecttaskvm);
                _context.SaveChanges();
                _dto.message = "success";
                if (Request.IsAjaxRequest())
                {
                    _dto.id = projecttaskvm.ProjectId;
                    return Json(_dto, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Details", "Project", new { id = projecttaskvm.ProjectId });
                }
            }
            return View(projecttaskvm);
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

        [HttpPost]
        public ActionResult AddTask(ProjectTaskViewModel model)
        {
            var project = _context.Projects.Include(x => x.ProjectTasks).FirstOrDefault(d => d.Id == model.ProjectId);
            bool successful = false;
            if (project != null)
            {
                project.ProjectTasks.Add(new ProjectTask
                {
                    TaskName = model.TaskName,
                    Estimated = new Estimated
                    {
                        Budget = model.Budget,
                        EndDate = DateTime.Parse(model.EndDate),
                        StartDate = DateTime.Parse(model.StartDate)
                    }
                });

            }

            try
            {
                _context.Entry(project).State = EntityState.Modified;
                _context.SaveChanges();
                successful = true;
            }
            catch (Exception)
            {

                successful = false;
            }
            return Json(successful,JsonRequestBehavior.AllowGet);
        }

        #region HighCharts

        #region 1.0 Requesting All Tasks For A Given Project
        public JsonResult GetAllTasks(int projectId)
        {
            var _tasks = _context
                .ProjectTasks
                .Where(t => t.ProjectId == projectId);
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}
