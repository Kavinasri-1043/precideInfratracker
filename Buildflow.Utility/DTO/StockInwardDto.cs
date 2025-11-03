using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Utility.DTO
{
    public class StockInwardDto
    {
        public int ProjectId { get; set; }
        public string? GRN { get; set; }
        public string? ItemName { get; set; }
        public int VendorId { get; set; }           // Vendor from ProjectTeam
        public int QuantityReceived { get; set; }
        public string? Unit { get; set; }           // e.g., Bags, Tons, Units
        public DateTime DateReceived { get; set; }
        public int ReceivedById { get; set; }       // Engineer from ProjectTeam
        public string? Status { get; set; }         // Approved / Pending / Rejected
        public string? Remarks { get; set; }
    }
}
