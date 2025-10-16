using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Helper
{
    public static class HospitalModelExtension
    {
        public static HospitalViewModel ToHospitalViewModel(this Hospital hospital, string lang)
        {
            if (lang == "vi")
            {
                return new HospitalViewModel
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
                    Note = hospital.Note,
                    Latitude = hospital.Latitude,
                    Longitude = hospital.Longitude
                };
            }
            else
            {
                return new HospitalViewModel
                {
                    HospitalId = hospital.HospitalId,
                    HospitalName = hospital.HospitalNameEn ?? string.Empty,
                    HospitalAddress = hospital.HospitalAddressEn ?? string.Empty,
                    IsPublicHospital = hospital.IsPublicHospital ?? false,
                    InPatient = hospital.InPatient ?? false,
                    OutPatient = hospital.OutPatient ?? false,
                    Dental = hospital.Dental ?? false,
                    PhoneNumber = hospital.PhoneNumber,
                    BillingTime = hospital.BillingTimeEn,
                    InsuranceAndDirectBilling = hospital.InsuranceAndDirectBillingEn,
                    Note = hospital.NoteEn,
                    Latitude = hospital.Latitude,
                    Longitude = hospital.Longitude
                };
            }
        }


        public static CreateHospitalViewModel ToCreateHospitalViewModel(this Hospital hospital)
        {
            return new CreateHospitalViewModel
            {
                HospitalName = hospital.HospitalName,
                HospitalAddress = hospital.HospitalAddress,
                IsPublicHospital = hospital.IsPublicHospital ?? false,
                InPatient = hospital.InPatient ?? false,
                OutPatient = hospital.OutPatient ?? false,
                Dental = hospital.Dental ?? false,
                PhoneNumber = hospital.PhoneNumber,
                BillingTime = hospital.BillingTime,
                InsuranceAndDirectBilling = hospital.InsuranceAndDirectBilling,
                Note = hospital.Note,
                Latitude = hospital.Latitude,
                Longitude = hospital.Longitude,
                IsBlackList = hospital.IsBlackList ?? false,
            };
        }
        public static EditHospitalViewModel ToEditHospitalViewModel(this Hospital hospital, string lang,string cityName,string districtName,string wardName,int currentPage)
        {
            if (lang == "vi")
            {
                return new EditHospitalViewModel
                {
                    HospitalId = hospital.HospitalId,
                    HospitalName = hospital.HospitalName ?? string.Empty,
                    HospitalAddress = hospital.HospitalAddress ?? string.Empty,
                    IsPublicHospital = hospital.IsPublicHospital ?? false,
                    InPatient = hospital.InPatient ?? false,
                    OutPatient = hospital.OutPatient ?? false,
                    Dental = hospital.Dental ?? false,
                    PhoneNumber = hospital.PhoneNumber ?? string.Empty,
                    BillingTime = hospital.BillingTime ?? string.Empty,
                    InsuranceAndDirectBilling = hospital.InsuranceAndDirectBilling ?? string.Empty,
                    Note = hospital.Note ?? string.Empty,
                    IsBlackList = hospital.IsBlackList ?? false,
                    CityId = hospital.CityId,
                    DistrictId = hospital.DistrictId,
                    WardId = hospital.WardId,
                    CityName = cityName,
                    DistrictName = districtName,
                    WardName = wardName,
                    CurrentPage = currentPage,
                };
            }
            else
            {
                return new EditHospitalViewModel
                {
                    HospitalId = hospital.HospitalId,
                    HospitalName = hospital.HospitalNameEn ?? string.Empty,
                    HospitalAddress = hospital.HospitalAddressEn ?? string.Empty,
                    IsPublicHospital = hospital.IsPublicHospital ?? false,
                    InPatient = hospital.InPatient ?? false,
                    OutPatient = hospital.OutPatient ?? false,
                    Dental = hospital.Dental ?? false,
                    PhoneNumber = hospital.PhoneNumber ?? string.Empty,
                    BillingTime = hospital.BillingTimeEn ?? string.Empty,
                    InsuranceAndDirectBilling = hospital.InsuranceAndDirectBillingEn ?? string.Empty,
                    Note = hospital.NoteEn ?? string.Empty,
                    IsBlackList = hospital.IsBlackList ?? false,
                    CityId = hospital.CityId,
                    DistrictId = hospital.DistrictId,
                    WardId = hospital.WardId,
                    CityName = cityName,
                    DistrictName = districtName,
                    WardName = wardName,
                    CurrentPage = currentPage,
                };
            }

        }
        public static CityDTO ToCityDTO(this City city, string lang)
        {
            if (lang == "vi")
            {
                return new CityDTO(city.CityName ?? string.Empty, city.CityId);
            }
            else
            {
                return new CityDTO(city.CityNameEn ?? string.Empty, city.CityId);
            }
        }
        public static DistrictDTO ToDistrictDTO(this District district, string lang)
        {
            if (lang == "vi")
            {
                return new DistrictDTO(district.DistrictName ?? string.Empty, district.DistrictId);
            }
            else
            {
                return new DistrictDTO(district.DistrictNameEn ?? string.Empty, district.DistrictId);
            }
        }
        public static WardDTO ToWardDTO(this Ward ward, string lang)
        {
            if (lang == "vi")
            {
                return new WardDTO(ward.WardName ?? string.Empty, ward.WardId);
            }
            else
            {
                return new WardDTO(ward.WardNameEn ?? string.Empty, ward.WardId);
            }
        }
        public static string ConvertBooltoString(this bool value, string trueOption, string falseOption)
        {
            return value ? trueOption : falseOption;
        }

        public static InsuranceViewModel ToInsuranceViewModel(
            this PagedList<HospitalViewModel> hospitalPagedList,
            string currentHospitalName = "",
            string currentCityName = "",
            string currentDistrictName = "",
            string currentWardName = "",
            bool? currentIsPublicHosptial = null,
            bool? currentInPatient = null,
            bool? currentOutPatient = null,
            bool? currentDental = null,
            string currentSortOrder = "default",
            bool currentIsBlackList = false,
            string currentLanguage = "vi")
        {
            return new InsuranceViewModel
            {
                HospitalPageList = hospitalPagedList.Items,
                CurrentPage = hospitalPagedList.Page,
                TotalRecord = hospitalPagedList.TotalCount,
                CurrentHospitalName = currentHospitalName,
                CurrentCityName = currentCityName,
                CurrentDistrictName = currentDistrictName,
                CurrentWardName = currentWardName,
                CurrentIsPublicHospital = currentIsPublicHosptial,
                CurrentInPatient = currentInPatient,
                CurrentOutPatient = currentOutPatient,
                CurrentDental = currentDental,
                CurrentSortOrder = currentSortOrder,
                CurrentIsBlackList = currentIsBlackList,
                CurrentLanguage = currentLanguage,
            };
        }
        public static SearchViewModel ToSearchViewModel(this Hospital hospital, string lang)
        {
            if (lang == "vi")
            {
                return new SearchViewModel
                {
                    ItemName = hospital.HospitalName
                };
            }
            else
            {
                return new SearchViewModel
                {
                    ItemName = hospital.HospitalNameEn ?? string.Empty
                };
            }
        }


        public static SearchViewModel ToSearchViewModel(this string itemName)
        {
            return new SearchViewModel { ItemName = itemName };
        }
        public static List<SearchViewModel> ToSearchViewModelList(this List<string> itemList)
        {
            return itemList.Select(h => h.ToSearchViewModel())
                                   .ToList();
        }

        public static SearchDTO toSearchDTO(this List<string> searchViewModelList)
        {
            return new SearchDTO(searchViewModelList);
        }


        public static HospitalDTO ToHospitalDTO(this Hospital hospital, string lang)
        {
            if (lang == "vi")
            {
                return new HospitalDTO
                {
                   HospitalName= hospital.HospitalName,
                   HospitalAddress= hospital.HospitalAddress,
                   Latitude= hospital.Latitude ?? 0,
                   Longitude = hospital.Longitude ?? 0
                };
            }
            else
            {
                return new HospitalDTO
                {
                    HospitalName = hospital.HospitalNameEn ?? string.Empty,
                    HospitalAddress= hospital.HospitalAddressEn ?? string.Empty,
                    Latitude = hospital.Latitude ?? 0,
                    Longitude = hospital.Longitude ?? 0
                };
            }

        }
        public static InsuranceDTO ToInsuranceDTO(this PagedList<HospitalViewModel> hospitalPagedList)
        {
            return new InsuranceDTO(hospitalPagedList.Items, hospitalPagedList.TotalCount);
        }

        public static MapDTO ToMapDTO(this List<HospitalDTO> hospitalDTOs)
        {
            return new MapDTO(hospitalDTOs);
        }


        public static FileResponseDTO ToFileResponseDTO(this Hospital hospital, string lang,string cityName,string districtName,string wardName)
        {
            if(lang == "vi")
            {
                return new FileResponseDTO(hospital.HospitalName, 
                    hospital.HospitalAddress, 
                    hospital.PhoneNumber, 
                    hospital.BillingTime, 
                    hospital.InsuranceAndDirectBilling, 
                    hospital.Note, 
                    cityName, 
                    districtName, 
                    wardName, 
                    hospital.IsPublicHospital, 
                    hospital.InPatient, 
                    hospital.OutPatient, 
                    hospital.Dental);
            }
                return new FileResponseDTO(hospital.HospitalNameEn ?? string.Empty, 
                    hospital.HospitalAddressEn ?? string.Empty, 
                    hospital.PhoneNumber, 
                    hospital.BillingTimeEn ?? string.Empty, 
                    hospital.InsuranceAndDirectBillingEn ?? string.Empty, 
                    hospital.NoteEn ?? string.Empty, 
                    cityName, 
                    districtName, 
                    wardName, 
                    hospital.IsPublicHospital, 
                    hospital.InPatient, 
                    hospital.OutPatient, 
                    hospital.Dental);
            
        }
    }
}
