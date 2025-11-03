using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Infrastructure.Entities
{
    public class StockInward
    {
        public int StockInwardId { get; set; }
        public int ProjectId { get; set; }
        public string? GRN { get; set; }
        public string? ItemName { get; set; }
        public int? VendorId { get; set; }
        public decimal? QuantityReceived { get; set; }
        public string? Unit { get; set; }
        public DateTime? DateReceived { get; set; }

        // ✅ Add this field to match your modelBuilder
        public int? ReceivedById { get; set; }

        public string? Status { get; set; }
        public string? Remarks { get; set; }

        // ✅ Navigation properties
        public virtual Vendor? Vendor { get; set; }
        public virtual Project? Project { get; set; }
        public virtual EmployeeDetail? ReceivedByEmployee { get; set; } // 👈 Add this
    }
}