using System;
using System.Collections.Generic;

namespace InsuranceWebApp.Data;

public partial class Ward
{
    public int WardId { get; set; }

    public string? WardName { get; set; }

    public string? WardNameEn { get; set; }

    public int? DistrictId { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
