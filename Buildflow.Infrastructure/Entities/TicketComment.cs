using System;
using System.Collections.Generic;

namespace Buildflow.Infrastructure.Entities;

public partial class TicketComment
{
    public int CommentId { get; set; }

    public int? TicketId { get; set; }

    public string? Comment { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual EmployeeDetail? CreatedByNavigation { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
