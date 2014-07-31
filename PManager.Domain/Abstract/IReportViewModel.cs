using System.Linq;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;

namespace PManager.Domain.Abstract
{
    public interface IReportViewModel
    {
        IQueryable<Project> GetProjects(ReportType reportType, EFDbContext context);
    }
}
