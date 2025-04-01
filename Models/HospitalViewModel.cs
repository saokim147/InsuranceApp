using System.ComponentModel.DataAnnotations;

namespace InsuranceWebApp.Models
{
    public class HospitalViewModel
    {
        public int HospitalId { get; set; }
        [Required]
        public string HospitalName { get; set; } = string.Empty;
        [Required]
        public string HospitalAddress { get; set; } = string.Empty;
        public bool IsPublicHospital { get; set; }
        public bool InPatient { get; set; }
        public bool OutPatient { get; set; }
        public bool Dental { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BillingTime { get; set; }
        public string? InsuranceAndDirectBilling { get; set; }
        public string? Note { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
