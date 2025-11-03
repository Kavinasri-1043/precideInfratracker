using System;
using System.Collections.Generic;

namespace Buildflow.Infrastructure.Entities;

public partial class Attachment1
{
    public int AttachmentId { get; set; }

    public int? ReportId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public virtual Report? Report { get; set; }
}
