using System;
using System.Collections.Generic;

namespace InsuranceWebApp.Data;

public partial class District
{
    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public string? DistrictNameEn { get; set; }

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
