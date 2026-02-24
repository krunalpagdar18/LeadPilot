using System;
using System.Collections.Generic;

namespace LeadPilot.Models;

public partial class EmailTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int EmailTypeId { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool Active { get; set; }

    public int? SouceId { get; set; }

    public virtual EmailType EmailType { get; set; } = null!;

    public virtual ICollection<LeadEmailLog> LeadEmailLogs { get; set; } = new List<LeadEmailLog>();

    public virtual LeadSource? Souce { get; set; }
}
