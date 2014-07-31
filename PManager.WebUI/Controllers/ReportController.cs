using System.Linq;
using System.Web.Mvc;
using PManager.Domain.Abstract;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;
using PManager.WebUI.DTOS;

namespace PManager.WebUI.Controllers
{
    public class ReportController: Controller
    {
        private readonly EFDbContext _db = new EFDbContext();
        private readonly IReportViewModel _report;
        private IQueryable<Project> _projects;

        public ReportController(IReportViewModel reportParam)
        {
            _report = reportParam;
        }

        public ActionResult ManagerReport(string filter = null)
        {
            switch (filter)
            {
                case "annual":
                    _projects = _report.GetProjects(ReportType.Annual, _db);
                    break;

                case "bi-annual":
                    _projects = _report.GetProjects(ReportType.Biannual, _db);
                    break;

                case "monthly":
                    _projects = _report.GetProjects(ReportType.Monthly, _db);
                    break;

                case "weekly":
                    _projects = _report.GetProjects(ReportType.Weekly, _db);
                    break;
            }
            ViewBag.filter = filter;
            return View(_projects);
        }

        public JsonResult GetReportsData(string filter = null)
        {
            switch (filter)
            {
                case "annual":
                    _projects = _report.GetProjects(ReportType.Annual, _db);
                    break;

                case "bi-annual":
                    _projects = _report.GetProjects(ReportType.Biannual, _db);
                    break;

                case "monthly":
                    _projects = _report.GetProjects(ReportType.Monthly, _db);
                    break;

                case "weekly":
                    _projects = _report.GetProjects(ReportType.Weekly, _db);
                    break;
            }

            var projectsToRender = from project in _projects
            select new
            {
                project.Id,
                project.ProjectCode,
                project.Estimated.Budget,
                ProjectTasks = project.ProjectTasks.Select(p => new
                {
                    p.Actual,
                    p.Estimated,
                    p.Id,
                    p.IsCompleted,
                    p.ProjectId,
                    p.TeamId
                })
            };
            var reports = projectsToRender.Select(project => new ReportDto
            {
                ReportId = project.Id,
                ReportCode = project.ProjectCode,
                CompletedTasks = project.ProjectTasks.Count(t => t.IsCompleted),
                IncompleteTasks = project.ProjectTasks.Count(t => !t.IsCompleted)

            }).ToList();
            return Json(reports, JsonRequestBehavior.AllowGet);
        }
    }
}