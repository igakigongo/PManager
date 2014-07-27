using System;
using System.Web.Mvc;
using PManager.Domain.Abstract;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;

namespace PManager.WebUI.Controllers
{
    public class ReportController: Controller
    {
        private readonly EFDbContext _db = new EFDbContext();
        private IReportViewModel _report;

        public ReportController(IReportViewModel reportParam)
        {
            _report = reportParam;
        }

        public ActionResult ManagerReport(string filter = null)
        {
            if (String.IsNullOrEmpty(filter)) return View();
            var model;
            switch (filter)
            {
                case "":
                    model = _report.GetProjects(ReportType.Annual);
            }
            var model = _report.GetProjects()
            return View(model);
        }
    }
}