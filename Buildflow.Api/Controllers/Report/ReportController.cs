using Buildflow.Api.Model;
using Buildflow.Infrastructure.DatabaseContext;
using Buildflow.Infrastructure.Entities;
using Buildflow.Service.Service.Project;
using Buildflow.Service.Service.Report;
using Buildflow.Utility.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buildflow.Api.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        private readonly ReportService _service;
        private readonly IConfiguration _config;
        private readonly BuildflowAppContext _context;





        public ReportController(ReportService service, IConfiguration config, BuildflowAppContext context)
        {
            _service = service;
            _config = config;
            _context = context;
            //_env = env;
            //_logger = logger;
        }


        [HttpPost("upsert-report")]
        public async Task<IActionResult> UpsertReport(Buildflow.Infrastructure.Entities.Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpsertReportAsync(report);
            return Ok(new
            {
                message = "Report saved successfully.",
                reportId = report.ReportId
            });
        }

        [HttpGet("getreport")]
        public async Task<IActionResult> GetReport()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reports = await _service.GetReportsAsync();

            return Ok(new
            {
                message = "Reports fetched successfully.",
                data = reports
            });
        }

        [HttpGet("getreportbyid")]
        public async Task<IActionResult> GetReportById(int reportid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = await _service.GetReportByIdAsync(reportid);

            if (report == null)
                return NotFound(new { message = $"Report with ID {reportid} not found." });

            return Ok(new
            {
                message = "Report fetched successfully.",
                data = report
            });
        }

        [HttpGet("getreportattachmentbyid")]
        public async Task<IActionResult> GetReportAttachmentById(int reportid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = await _service.GetReportAttachmentByIdAsync(reportid);

            if (report == null)
                return NotFound(new { message = $"Report with ID {reportid} not found." });

            return Ok(new
            {
                message = "Report Attachments fetched successfully.",
                data = report
            });

        }
        [HttpPost("upload-attachments")]
        public async Task<IActionResult> UploadAttachments(int reportId, List<IFormFile> files)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound("Report not found.");

            // Define the folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "report");

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(folderPath, fileName);
                    var relativePath = Path.Combine("uploads", "report", fileName); // for storing in DB

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    var attachment = new Attachment1
                    {
                        ReportId = reportId,
                        FileName = fileName,
                        FilePath = relativePath, // store relative path instead of full system path
                        UploadedAt = DateTime.Now
                    };

                    await _context.Attachments1.AddAsync(attachment);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Attachments uploaded successfully." });
        }
    }
}
