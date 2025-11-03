using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Utility.DTO
{
    public class StockOutwardDto
    {
        public int ProjectId { get; set; }
        public string? IssueNo { get; set; }    // ✅ Add this line
        public string? ItemName { get; set; }
        public int? RequestedById { get; set; }
        public decimal? IssuedQuantity { get; set; }
        public string? Unit { get; set; }
        public int? IssuedToId { get; set; }
        public DateTime? DateIssued { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
    }
}
