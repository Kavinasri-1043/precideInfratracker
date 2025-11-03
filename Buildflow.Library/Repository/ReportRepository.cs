using Buildflow.Infrastructure.DatabaseContext;
using Buildflow.Infrastructure.Entities;
using Buildflow.Library.Repository.Interfaces;
using Buildflow.Utility.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Buildflow.Library.Repository
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {

        private readonly ILogger<GenericRepository<Infrastructure.Entities.Report>> _logger;

        private readonly IConfiguration _configuration;
        private readonly BuildflowAppContext _context;

        public ReportRepository(IConfiguration configuration, BuildflowAppContext context, ILogger<GenericRepository<Infrastructure.Entities.Report>> logger)
            : base(context, logger)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public async Task<Report> GetReportByIdAsync(int reportId)
        {
            return await _context.Reports
                .Include(r => r.Attachment1s)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        public async Task<List<Attachment1>> GetReportAttachmentByIdAsync(int reportId)
        {
            return await _context.Attachments1
                .AsNoTracking()
               .Where(r => r.ReportId == reportId).ToListAsync();
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _context.Reports
                .AsNoTracking()
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
        }

        public async Task UpsertReportAsync(Buildflow.Infrastructure.Entities.Report report)
        {
            if (report.ReportId > 0)
            {
                // UPDATE EXISTING REPORT
                var existing = await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == report.ReportId);

                if (existing != null)
                {
                    existing.ReportType = report.ReportType;
                    existing.ProjectId = report.ProjectId;
                    existing.ReportDate = report.ReportDate;
                    existing.ReportedBy = report.ReportedBy;
                    existing.ReportData = report.ReportData;
                    //existing.ReportData = JsonConvert.SerializeObject(report.ReportData);
                    //                entity.Property(r => r.ReportData)
                    //.HasConversion(
                    //    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    //    v => JsonSerializer.Deserialize<ReportData>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));


                    //            });
                    existing.UpdatedAt = DateTime.Now;

                    _context.Reports.Update(existing);
                }
            }
            else
            {
                report.CreatedAt = DateTime.Now;
                report.UpdatedAt = DateTime.Now;

                await _context.Reports.AddAsync(report);
            }

            await _context.SaveChangesAsync();
        }

    }
}
