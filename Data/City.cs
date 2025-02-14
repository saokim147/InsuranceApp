namespace InsuranceWebApp.Data
{
    public partial class City
    {
        public string CityName { get; set; } = null!;

        public int CityId { get; set; }

        public virtual ICollection<District> Districts { get; set; } = [];

        public virtual ICollection<Hospital> Hospitals { get; set; } = [];
    }
}

