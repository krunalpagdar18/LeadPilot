using System;
using System.Collections.Generic;

namespace LeadPilot.Models;

public partial class EmailType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();
}
