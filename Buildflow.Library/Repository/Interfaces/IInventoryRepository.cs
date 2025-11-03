using Buildflow.Infrastructure.Entities;
using Buildflow.Utility.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Library.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<EmployeeDetail>> GetProjectTeamMembersAsync(int projectId);
        Task<StockInward> CreateStockInwardAsync(StockInward stockInward);
        Task<StockOutward> CreateStockOutwardAsync(StockOutward stockOutward);
    }
}
