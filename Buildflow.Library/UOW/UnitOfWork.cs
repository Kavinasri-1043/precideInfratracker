using Buildflow.Infrastructure.DatabaseContext;
using Buildflow.Infrastructure.Entities;
using Buildflow.Library.Repository;
using Buildflow.Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Buildflow.Library.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuildflowAppContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IConfiguration _configuration;

        public IProjectRepository Boq { get; private set; }
        public IReportRepository ReportRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IProjectRepository ProjectTeam { get; private set; }
        public IRegisterRepository Employees { get; private set; }
        public IRegisterRepository LoginEmployee { get; private set; }
        public IRegisterRepository EmployeeRoles { get; private set; }
        public IRegisterRepository RegisterUser { get; private set; }
        public IRegisterRepository VendorDetails { get; private set; }
        public IRegisterRepository SubcontractorDetails { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IVendorRepository Vendors { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IProjectRepository ProjectTypes { get; private set; }
        public IProjectRepository ProjectSectors { get; private set; }
        public IProjectRepository ProjectBudgets { get; private set; }
        public IProjectRepository ProjectMilestone { get; private set; }
        public ITicketRepository TicketRepository { get; private set; }
        public IInventoryRepository InventoryRepository { get; private set; }
        public IProjectRepository ProjectPermissionFinanceApprovals { get; private set; }
        public IProjectRepository ProjectMilestones { get; private set; }

        public UnitOfWork(
            BuildflowAppContext context,
            IConfiguration configuration,
            ILogger<UnitOfWork> logger,
            ILogger<RegisterRepository> registerLogger,
            ILogger<ProjectRepository> projectLogger,
            ILogger<GenericRepository<Notification>> notificationLogger,
            ILogger<GenericRepository<EmployeeDetail>> employeeLogger,
            ILogger<GenericRepository<Report>> reportLogger,
            ILogger<GenericRepository<Ticket>> ticketLogger,
            ILogger<GenericRepository<Vendor>> vendorLogger,
            ILogger<InventoryRepository> inventoryLogger,  // ✅ Added
            IRoleRepository roles,
            IDepartmentRepository depts
        )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;

            // Initialize repositories
            TicketRepository = new TicketRepository(_configuration, _context, ticketLogger);
            EmployeeRepository = new EmployeeRepository(_configuration, _context, employeeLogger);
            ReportRepository = new ReportRepository(_configuration, _context, reportLogger);
            Boq = new ProjectRepository(_configuration, _context, projectLogger);
            Roles = roles;
            Vendors = new VendorRepository(_configuration, _context, vendorLogger);
            DepartmentRepository = depts;

            // Register repositories
            VendorDetails = new RegisterRepository(_configuration, _context, registerLogger);
            SubcontractorDetails = new RegisterRepository(_configuration, _context, registerLogger);
            Employees = new RegisterRepository(_configuration, _context, registerLogger);
            RegisterUser = new RegisterRepository(_configuration, _context, registerLogger);
            EmployeeRoles = new RegisterRepository(_configuration, _context, registerLogger);
            LoginEmployee = new RegisterRepository(_configuration, _context, registerLogger);

            // Project-related repositories
            Projects = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectTypes = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectSectors = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectBudgets = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectMilestone = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectTeam = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectPermissionFinanceApprovals = new ProjectRepository(_configuration, _context, projectLogger);
            ProjectMilestones = new ProjectRepository(_configuration, _context, projectLogger);

            // Notification + Inventory
            NotificationRepository = new NotificationRepository(_configuration, _context, notificationLogger);
            InventoryRepository = new InventoryRepository(_configuration, _context, inventoryLogger);

        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
