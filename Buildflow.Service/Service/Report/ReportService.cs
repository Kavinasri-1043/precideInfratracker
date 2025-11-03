using Buildflow.Infrastructure.Entities;
using Buildflow.Library.UOW;
using Buildflow.Utility.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Service.Service.Report
{
    public class ReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpsertReportAsync(Buildflow.Infrastructure.Entities.Report report)
        {
            await _unitOfWork.ReportRepository.UpsertReportAsync(report);
        }

        public async Task<List<Buildflow.Infrastructure.Entities.Report>> GetReportsAsync()
        {
            return await _unitOfWork.ReportRepository.GetReportsAsync();
        }

        public async Task<Buildflow.Infrastructure.Entities.Report> GetReportByIdAsync(int reportid)
        {
            return await _unitOfWork.ReportRepository.GetReportByIdAsync(reportid);
        }
        public async Task<List<Attachment1>> GetReportAttachmentByIdAsync(int reportid)
        {
            return await _unitOfWork.ReportRepository.GetReportAttachmentByIdAsync(reportid);
        }
        
    }
}
