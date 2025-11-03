using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Infrastructure.Entities
{
    public class StockOutward
    {
        public int StockOutwardId { get; set; }
        public int ProjectId { get; set; }
        public string? IssueNo { get; set; }
        public string? ItemName { get; set; }
        public int? RequestedById { get; set; }
        public int? IssuedToId { get; set; }
        public string? Unit { get; set; }
        public decimal? IssuedQuantity { get; set; }
        public DateTime? DateIssued { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }

        // ✅ Add navigation properties to EmployeeDetail
        public virtual EmployeeDetail? RequestedByEmployee { get; set; }
        public virtual EmployeeDetail? IssuedToEmployee { get; set; }

        // Optional relationships to Project
        public virtual Project? Project { get; set; }
    }
}
