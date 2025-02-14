namespace InsuranceWebApp.Repository
{
    public class HospitalFindCreterias
    {
        public string HospitalName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        public bool IsAdvancedSearch { get; set; } = false;
        public bool IsPublicHospital { get; set; } = true;
        public bool InPatient { get; set; } = true;
        public bool OutPatient { get; set; } = true;
        public bool Dental { get; set; } = true;

    }
}
