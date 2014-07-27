using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;

namespace PManager.Domain.Abstract
{
    public interface IReportViewModel
    {
        IQueryable<Project> GetProjects(ReportType reportType, EFDbContext context);
    }
}
