using Buildflow.Infrastructure.DatabaseContext;
using Buildflow.Utility.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Library.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly BuildflowAppContext _context;

        public MaterialRepository(BuildflowAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetch materials for a given project and calculate stock level dynamically.
        /// </summary>
        public async Task<IEnumerable<MaterialDto>> GetMaterialsByProjectIdAsync(int projectId)
        {
            // Join BOQ, BOQ Items, StockInward, and BoqApproval
            var materials = await (from boq in _context.Boqs
                                   join item in _context.BoqItems on boq.BoqId equals item.BoqId
                                   join approval in _context.BoqApprovals on boq.BoqId equals approval.BoqId into approvals
                                   from approval in approvals.DefaultIfEmpty()
                                   join stock in _context.StockInwards on item.ItemName equals stock.ItemName into stocks
                                   from stock in stocks.DefaultIfEmpty()
                                   where boq.ProjectId == projectId
                                   select new MaterialDto
                                   {
                                       MaterialList = item.ItemName,
                                       InStockQuantity = stock != null ? Convert.ToInt32(stock.QuantityReceived) : 0,
                                       RequiredQuantity = item.Quantity ?? 0,
                                       Level = (item.Quantity > Convert.ToInt32(stock.QuantityReceived))
                                                ? "High"
                                                : (item.Quantity == Convert.ToInt32(stock.QuantityReceived))
                                                    ? "Medium"
                                                    : "Low",
                                       RequestStatus = approval.ApprovalStatus ?? "Pending"
                                   }).ToListAsync();

            return materials;
        }
    }
}

        
    

   
