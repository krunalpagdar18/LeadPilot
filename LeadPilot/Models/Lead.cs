using System;
using System.Collections.Generic;

namespace LeadPilot.Models;

public partial class Lead
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? Website { get; set; }

    public string City { get; set; } = null!;

    public int? SourceId { get; set; }

    public int? StatusId { get; set; }

    public DateOnly? AddedOn { get; set; }

    public string EmailId { get; set; } = null!;

    public bool Inactive { get; set; }

    public DateTime? InactiveDate { get; set; }

    public virtual ICollection<LeadEmailLog> LeadEmailLogs { get; set; } = new List<LeadEmailLog>();

    public virtual LeadSource? Source { get; set; }

    public virtual LeadStatus? Status { get; set; }
}
