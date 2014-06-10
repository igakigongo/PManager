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
        private IDataTransferObject _dto;
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public ProjectController(IDataTransferObject _dtoParam)
        {
            this._dto = _dtoParam;
        }

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
            // Loading Project Tasks - Eager Loading
            project.ProjectTasks = _unitOfWork.ProjectTaskRepository
                                            .Get()
                                            .Where(p => p.ProjectId == project.Id)
                                            .OrderByDescending(p => p.Estimated.StartDate)
                                            .ToList();
            // Loading Users - Eager Loading
            project.ProjectTasks.ForEach(_team => _team.Team.ForEach(_teammember => _teammember = _unitOfWork.UserRepository.Find(_teammember.Id)));
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
                    _unitOfWork.ProjectRepository.Add(project);
                    _unitOfWork.Save();
                    _dto.message = "success";
                    _dto.id = _unitOfWork.ProjectRepository.Get().LastOrDefault().Id;
                }
                catch (DbUpdateException ex)
                {
                    _dto.message = "Error: " + ex.Message;
                }
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
        public ActionResult Edit(Project project)
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


        #region HighCharts

        #region 1.0 Recent Top Ten Project Costing
        public JsonResult TopTenProjectsCosting()
        {
            IQueryable<Project> _TopTen = _unitOfWork.ProjectRepository.Get().OrderByDescending(p => p.Estimated.StartDate).Take(10).AsQueryable();
            return Json(_TopTen.ToArray(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}
