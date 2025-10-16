namespace InsuranceWebApp.Data;
public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public string? CityNameEn { get; set; }

    public virtual ICollection<District> Districts { get; set; } = [];

    public virtual ICollection<Hospital> Hospitals { get; set; } = [];
}
