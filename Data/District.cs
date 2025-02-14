
namespace InsuranceWebApp.Data
{
    public partial class District
    {
        public string DistrictName { get; set; } = null!;

        public int? CityId { get; set; }

        public int DistrictId { get; set; }

        public virtual City? City { get; set; }

        public virtual ICollection<Hospital> Hospitals { get; set; } = [];

        public virtual ICollection<Ward> Wards { get; set; } = [];
    }
}

