using Buildflow.Infrastructure.Entities;
using Buildflow.Library.Repository.Interfaces;
using Buildflow.Library.UOW;
using Buildflow.Utility.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Service.Service.Inventory 
{ 

        public interface IInventoryService
        {
        Task<IEnumerable<EmployeeDetail>> GetProjectTeamMembersAsync(int projectId);
        Task<StockInward> CreateStockInwardAsync(StockInwardDto dto);
        Task<StockOutward> CreateStockOutwardAsync(StockOutwardDto dto);
    }

        public class InventoryService : IInventoryService
        {
            private readonly IInventoryRepository _inventoryRepository;

            public InventoryService(IInventoryRepository inventoryRepository)
            {
                _inventoryRepository = inventoryRepository;
            }
        public async Task<IEnumerable<EmployeeDetail>> GetProjectTeamMembersAsync(int projectId)
        {
            return await _inventoryRepository.GetProjectTeamMembersAsync(projectId);
        }

        public async Task<StockInward> CreateStockInwardAsync(StockInwardDto dto)
        {
            var entity = new StockInward
            {
                ProjectId = dto.ProjectId,
                GRN = dto.GRN,
                ItemName = dto.ItemName,
                VendorId = dto.VendorId,
                QuantityReceived = dto.QuantityReceived,
                Unit = dto.Unit,
                DateReceived = dto.DateReceived,
                ReceivedById = dto.ReceivedById,
                Status = dto.Status,
                Remarks = dto.Remarks
            };

            return await _inventoryRepository.CreateStockInwardAsync(entity);
        }

        public async Task<StockOutward> CreateStockOutwardAsync(StockOutwardDto dto)
        {
            var entity = new StockOutward
            {
                ProjectId = dto.ProjectId,
                IssueNo = dto.IssueNo,
                ItemName = dto.ItemName,
                RequestedById = dto.RequestedById,
                IssuedQuantity = dto.IssuedQuantity,
                Unit = dto.Unit,
                IssuedToId = dto.IssuedToId,
                DateIssued = dto.DateIssued,
                Status = dto.Status,
                Remarks = dto.Remarks
            };

            return await _inventoryRepository.CreateStockOutwardAsync(entity);
        }
    }
}
