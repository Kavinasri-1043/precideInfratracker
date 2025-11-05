using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Utility.DTO
{
    public class MaterialDto
    {
        public string MaterialList { get; set; } = string.Empty;
        public int InStockQuantity { get; set; }
        public int RequiredQuantity { get; set; }
        public string Level { get; set; } = string.Empty;//Derived field (calculated)
        public string RequestStatus { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
