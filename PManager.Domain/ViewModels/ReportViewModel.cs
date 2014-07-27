using System;
using System.Data.Entity;
using System.Linq;
using PManager.Domain.Abstract;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;

namespace PManager.Domain.ViewModels
{
    public class ReportViewModel: IReportViewModel
    {
        private readonly EFDbContext _context;
        private IQueryable<Project> _projects;

        public ReportViewModel(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Project> GetProjects(ReportType reportType)
        {
            var currentDateTime = DateTime.Now;
            var year = currentDateTime.Year;
            var month = currentDateTime.Month;
            switch (reportType)
            {
                case ReportType.Annual:
                    _projects = _context.Projects.Include(p => p.ProjectTasks).Where(p => p.Actual.StartDate.Value.Year == DateTime.Now.Year);
                    break;

                case ReportType.Biannual:
                    _projects = (month < 7)
                        ? _context.Projects.Include(p => p.ProjectTasks)
                            .Where(p => p.Actual.StartDate.HasValue && p.Actual.StartDate.Value.Month < 7 && p.Actual.StartDate.Value.Year == year)
                        : _context.Projects.Include(p => p.ProjectTasks)
                            .Where(p => p.Actual.StartDate.HasValue && p.Actual.StartDate.Value.Month > 6 && p.Actual.StartDate.Value.Year == year);
                    break;

                case ReportType.Monthly:
                    break;

                case ReportType.Quarterly:
                    break;

                case ReportType.Weekly:
                    break;
            }
            return _projects;
        }
    }
}
