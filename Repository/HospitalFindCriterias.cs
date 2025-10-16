namespace InsuranceWebApp.Repository
{
    public class HospitalFindCriterias
    {
        public string HospitalName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        public bool? IsPublicHospital { get; set; }
        public bool? InPatient { get; set; } 
        public bool? OutPatient { get; set; } 
        public bool? Dental { get; set; } 
        public bool IsBlackList { get; set; } = false;
        public string Language { get; set; } = "vi";
    }
}
