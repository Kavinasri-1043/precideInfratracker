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
    public class InventoryMaterialRepository
    {
        private readonly BuildflowAppContext _context;

        public InventoryMaterialRepository(BuildflowAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryMaterialcsDto>> GetMaterialStatusAsync()
        {
            // Hardcoded minimum quantities
            var minimumQuantities = new Dictionary<string, int>
            {
                { "Cement (50kg)", 600 },
                { "Bricks", 600 },
                { "Rod", 600 },
                { "PVC Pipes", 600 },
                { "Wire (4mm)", 600 }
            };

            var materials = await _context.StockInwards
                .Select(i => new
                {
                    i.ItemName,
                    i.QuantityReceived
                })
                .ToListAsync();

            var result = materials
                .Where(m => minimumQuantities.ContainsKey(m.ItemName))
                .Select(m => new InventoryMaterialcsDto
                {
                    MaterialName = m.ItemName,
                    InStockQuantity =(int) m.QuantityReceived,
                    RequiredQuantity =minimumQuantities[m.ItemName] - (int)m.QuantityReceived
                })
                .ToList();

            // Include missing materials (not present in inventory)
            foreach (var min in minimumQuantities)
            {
                if (!result.Any(r => r.MaterialName == min.Key))
                {
                    result.Add(new InventoryMaterialcsDto
                    {
                        MaterialName = min.Key,
                        InStockQuantity = 0,
                        RequiredQuantity = min.Value
                    });
                }
            }

            return result;
        }
    }
}

