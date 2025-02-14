using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Helper
{
    public static class HospitalModelExtension
    {
        public static HospitalViewModel ToHospitalViewModel(this Hospital hospital)
        {
            return new HospitalViewModel
            {
                HospitalId=hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                HospitalAddress = hospital.HospitalAddress,
                IsPublicHospital = hospital.IsPublicHospital ?? false ,
                InPatient = hospital.InPatient ?? false,
                OutPatient = hospital.OutPatient ?? false,
                Dental = hospital.Dental ?? false,
                PhoneNumber = hospital.PhoneNumber,
                BillingTime = hospital.BillingTime,
                InsuranceAndDirectBilling = hospital.InsuranceAndDirectBilling,
                Note = hospital.Note,
                Latitude = hospital.Latitude,
                Longitude = hospital.Longitude
            };
        }
        public static EditHospitalViewModel ToEditHospitalViewModel(this Hospital hospital)
        {
            return new EditHospitalViewModel
            {
                HospitalId = hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                HospitalAddress = hospital.HospitalAddress,
                IsPublicHospital = hospital.IsPublicHospital ?? false,
                InPatient = hospital.InPatient ?? false,
                OutPatient = hospital.OutPatient ?? false,
                Dental = hospital.Dental ?? false,
                PhoneNumber = hospital.PhoneNumber,
                BillingTime = hospital.BillingTime,
                InsuranceAndDirectBilling = hospital.InsuranceAndDirectBilling,
                Note = hospital.Note
            };
        }
        public static CityDTO ToCityDTO(this City city)
        {
            return new CityDTO
            {
                CityName = city.CityName
            };
        }
        public static DistrictDTO ToDistrictDTO(this District district)
        {
            return new DistrictDTO
            {
                DistrictName = district.DistrictName
            };
        }
        public static WardDTO ToWardDTO(this Ward ward)
        {
            return new WardDTO
            {
                WardName = ward.WardName
            };
        }
        public static string ConvertBooltoString(this bool value, string trueOption, string falseOption)
        {
            return value ? trueOption : falseOption;
        }

    }
}
