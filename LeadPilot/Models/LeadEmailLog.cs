using System;
using System.Collections.Generic;

namespace LeadPilot.Models;

public partial class LeadEmailLog
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int EmailTemplateId { get; set; }

    public DateOnly? NextEmailDate { get; set; }

    public DateOnly MailDate { get; set; }

    public virtual EmailTemplate EmailTemplate { get; set; } = null!;

    public virtual Lead Lead { get; set; } = null!;
}
