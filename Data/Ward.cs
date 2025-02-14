namespace InsuranceWebApp.Data
{
    public partial class Ward
    {
        public string WardName { get; set; } = null!;

        public int? DistrictId { get; set; }

        public int WardId { get; set; }

        public virtual District? District { get; set; }

        public virtual ICollection<Hospital> Hospitals { get; set; } = [];
    }

}

