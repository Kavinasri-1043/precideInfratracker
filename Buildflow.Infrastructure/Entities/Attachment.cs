using System;
using System.Collections.Generic;

namespace Buildflow.Infrastructure.Entities;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public int TicketId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public int CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? TicketCommentId { get; set; }

    public virtual EmployeeDetail CreatedByNavigation { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual TicketComment? TicketComment { get; set; }
}
