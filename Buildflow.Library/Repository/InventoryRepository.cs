using Buildflow.Infrastructure.DatabaseContext;
using Buildflow.Infrastructure.Entities;
using Buildflow.Library.Repository.Interfaces;
using Buildflow.Utility.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Library.Repository
{
    public class InventoryRepository : IInventoryRepository

    {
        private readonly BuildflowAppContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(IConfiguration configuration, BuildflowAppContext context, ILogger<InventoryRepository> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IEnumerable<EmployeeDetail>> GetProjectTeamMembersAsync(int projectId)
        {
            // 1️⃣ Get the project team record for the given project
            var projectTeam = await _context.ProjectTeams
                .FirstOrDefaultAsync(pt => pt.ProjectId == projectId);

            if (projectTeam == null)
                return Enumerable.Empty<EmployeeDetail>();

            // 2️⃣ Combine all employee ID lists (ignore null lists)
            var allEmployeeIds = new List<int>();

            if (projectTeam.PmId != null) allEmployeeIds.AddRange(projectTeam.PmId);
            if (projectTeam.ApmId != null) allEmployeeIds.AddRange(projectTeam.ApmId);
            if (projectTeam.LeadEnggId != null) allEmployeeIds.AddRange(projectTeam.LeadEnggId);
            if (projectTeam.SiteSupervisorId != null) allEmployeeIds.AddRange(projectTeam.SiteSupervisorId);
            if (projectTeam.QsId != null) allEmployeeIds.AddRange(projectTeam.QsId);
            if (projectTeam.AqsId != null) allEmployeeIds.AddRange(projectTeam.AqsId);
            if (projectTeam.SiteEnggId != null) allEmployeeIds.AddRange(projectTeam.SiteEnggId);
            if (projectTeam.EnggId != null) allEmployeeIds.AddRange(projectTeam.EnggId);
            if (projectTeam.DesignerId != null) allEmployeeIds.AddRange(projectTeam.DesignerId);
            if (projectTeam.VendorId != null) allEmployeeIds.AddRange(projectTeam.VendorId);
            if (projectTeam.SubcontractorId != null) allEmployeeIds.AddRange(projectTeam.SubcontractorId);

            // 3️⃣ Remove duplicates (just in case)
            allEmployeeIds = allEmployeeIds.Distinct().ToList();

            // 4️⃣ Fetch all employee details for these IDs
            var employees = await _context.EmployeeDetails
                .Where(e => allEmployeeIds.Contains(e.EmpId)) // change EmployeeId if your PK is named differently
                .ToListAsync();

            return employees;
        }


        public async Task<StockInward> CreateStockInwardAsync(StockInward stockInward)
        {
            _context.StockInwards.Add(stockInward);
            await _context.SaveChangesAsync();
            return stockInward;
        }

        public async Task<StockOutward> CreateStockOutwardAsync(StockOutward stockOutward)
        {
            _context.StockOutwards.Add(stockOutward);
            await _context.SaveChangesAsync();
            return stockOutward;
        }
    }
}
