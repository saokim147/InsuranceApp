using System.ComponentModel.DataAnnotations;

namespace InsuranceWebApp.Models
{
    public class EditHospitalViewModel
    {
        public int HospitalId { get; set; }
        [Required(ErrorMessage = "Không để trống Tên Bệnh viện")]
        public string HospitalName { get; set; }= string.Empty;
        [Required(ErrorMessage = "Không để trống Địa chỉ")]
        public string HospitalAddress { get; set; } = string.Empty;
        public bool IsPublicHospital { get; set; }
        public bool InPatient { get; set; }
        public bool OutPatient { get; set; }
        public bool Dental { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BillingTime { get; set; }
        public string? InsuranceAndDirectBilling { get; set; }
        public string? Note { get; set; }
        public bool IsBlackList { get; set; } = false;
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public string? CityName { get; set; }
        public string? DistrictName { get; set; }
        public string? WardName { get; set; }
        public int CurrentPage { get; set; } = 1;
    }
}
