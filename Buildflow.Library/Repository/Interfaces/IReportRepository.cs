using Buildflow.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Library.Repository.Interfaces
{
    public interface IReportRepository
    {

        Task UpsertReportAsync(Buildflow.Infrastructure.Entities.Report report);
        Task<List<Buildflow.Infrastructure.Entities.Report>>  GetReportsAsync();
        Task<Buildflow.Infrastructure.Entities.Report>  GetReportByIdAsync(int reportid);
        Task<List<Attachment1>> GetReportAttachmentByIdAsync(int reportId);
    }
}
