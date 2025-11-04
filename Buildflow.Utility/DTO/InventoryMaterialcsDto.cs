using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Utility.DTO
{
    public  class InventoryMaterialcsDto
    {
        public string MaterialName { get; set; } = string.Empty;
        public int InStockQuantity { get; set; }
        public int RequiredQuantity { get; set; }
    }
}
