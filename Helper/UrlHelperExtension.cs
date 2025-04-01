using InsuranceWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebApp.Helper
{
    public static class UrlHelperExtension
    {
        public static string GenerateHospitalUrl(this IUrlHelper urlHelper,InsuranceViewModel model,int page=1,string sortState="")
        {
            var routeValues = new Dictionary<string, object> { };
            if (page != 1)
            {
                routeValues.Add("page", page);
            }
            routeValues.Add("hospitalName", model.CurrentHospitalName);
            routeValues.Add("cityName", model.CurrentCityName);
            routeValues.Add("districtName", model.CurrentDistrictName);
            routeValues.Add("wardName", model.CurrentWardName);
            if (model.CurrentIsPublicHospital.HasValue)
            {
                routeValues.Add("isPublicHospital", model.CurrentIsPublicHospital);
            }
            if (model.CurrentInPatient.HasValue)
            {
                routeValues.Add("inPatient", model.CurrentInPatient);
            }
            if (model.CurrentOutPatient.HasValue)
            {
                routeValues.Add("outPatient", model.CurrentOutPatient);
            }
            if (model.CurrentDental.HasValue)
            {
                routeValues.Add("dental", model.CurrentDental);
            }
            if(!string.IsNullOrEmpty(sortState))
            {
                routeValues.Add("sortOrder", sortState);
            }
            else
            {
                routeValues.Add("sortOrder", model.CurrentSortOrder);
            }
            routeValues.Add("isBlackList", model.CurrentIsBlackList);
            routeValues.Add("lang", model.CurrentLanguage);
            return urlHelper.Action("Index", "Hospital", routeValues) ?? string.Empty; 
        }
    }
}
