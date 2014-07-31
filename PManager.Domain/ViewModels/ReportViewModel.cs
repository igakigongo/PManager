using System.Data.Entity;
using System.Linq;
using PManager.Domain.Abstract;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;

namespace PManager.Domain.ViewModels
{
    public class ReportViewModel: IReportViewModel
    {
        private IQueryable<Project> _projects;

        public IQueryable<Project> GetProjects(ReportType reportType, EFDbContext context)
        {
            switch (reportType)
            {
                case ReportType.Annual:
                    _projects = context.Projects.Include(p => p.ProjectTasks);
                    break;

                case ReportType.Biannual:
                    _projects = context.Projects.Include(p => p.ProjectTasks);
                    break;

                case ReportType.Monthly:
                    _projects = context.Projects.Include(p => p.ProjectTasks);
                    break;

                case ReportType.Quarterly:
                    _projects = context.Projects.Include(p => p.ProjectTasks);
                    break;

                case ReportType.Weekly:
                    _projects = context.Projects.Include(p => p.ProjectTasks);
                    break;
            }
            return _projects;
        }
    
    }


}
