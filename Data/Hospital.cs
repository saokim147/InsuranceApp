namespace InsuranceWebApp.Data
{
    public partial class Hospital
    {
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        public string HospitalAddress { get; set; }= string.Empty;

        public bool? IsPublicHospital { get; set; }

        public bool? InPatient { get; set; }

        public bool? OutPatient { get; set; }

        public bool? Dental { get; set; }

        public string? PhoneNumber { get; set; }

        public string? BillingTime { get; set; }

        public string? InsuranceAndDirectBilling { get; set; }

        public string? Note { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public int? WardId { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public virtual City? City { get; set; }

        public virtual District? District { get; set; }

        public virtual Ward? Ward { get; set; }
    }
}

