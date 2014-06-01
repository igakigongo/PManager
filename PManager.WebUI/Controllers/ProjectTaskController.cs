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
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public ProjectTaskController(IDataTransferObject _dtoParam)
        {
            _dto = _dtoParam;
        }

        // GET: /ProjectTask/
        public ActionResult Index()
        {
            List<ProjectTask> projecttasks = _unitOfWork.ProjectTaskRepository.Get().ToList();
            projecttasks.ForEach(p => p.User = _unitOfWork.UserRepository.Find(p.UserId));
            projecttasks.ForEach(p => p.Project = _unitOfWork.ProjectRepository.Find(p.ProjectId));
            return View(projecttasks);
        }

        // GET: /ProjectTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projecttask = _unitOfWork.ProjectTaskRepository.Find(id);
            if (projecttask == null)
            {
                return HttpNotFound();
            }
            return View(projecttask);
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
                    _unitOfWork.ProjectTaskRepository.Add(projecttask);
                    _unitOfWork.Save();
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
            ProjectTask projecttask = _unitOfWork.ProjectTaskRepository.Find(id);
            if (projecttask == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProjectId = new SelectList(_unitOfWork.Projects, "Id", "ProjectCode", projecttask.ProjectId);
            ViewBag.UserId = new SelectList(_unitOfWork.UserRepository.Get(), "Id", "Fullname", projecttask.UserId);
            return View(projecttask);
        }

        // POST: /ProjectTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ProjectTask projecttask)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProjectTaskRepository.Update(projecttask);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.ProjectId = new SelectList(_unitOfWork.Projects, "Id", "ProjectCode", projecttask.ProjectId);
            ViewBag.UserId = new SelectList(_unitOfWork.UserRepository.Get(), "Id", "Fullname", projecttask.UserId);
            return View(projecttask);
        }

        // GET: /ProjectTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projecttask = _unitOfWork.ProjectTaskRepository.Find(id);
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
            ProjectTask projecttask = _unitOfWork.ProjectTaskRepository.Find(id);
            _unitOfWork.ProjectTaskRepository.Delete(projecttask);
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

        #region HighCharts 

        #region 1.0 Requesting All Tasks For A Given Project
        public JsonResult GetAllTasks(int projectId)
        {
            var _tasks = _unitOfWork.ProjectTaskRepository
                                        .Get()
                                        .Where(t => t.ProjectId == projectId)
                                        .OrderByDescending(t => t.Estimated.StartDate)
                                        .ToList();
            _tasks.ForEach(t => t.User = _unitOfWork.UserRepository.Find(t.UserId));
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}
