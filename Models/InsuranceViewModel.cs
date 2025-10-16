namespace InsuranceWebApp.Models
{
    public class InsuranceViewModel
    {
        public IEnumerable<HospitalViewModel> HospitalPageList { get; set; } = [];
        public int TotalRecord { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentHospitalName { get; set; }=string.Empty;
        public string CurrentCityName { get; set; }=string.Empty;
        public string CurrentDistrictName { get; set; }= string.Empty;
        public string CurrentWardName { get; set; } = string.Empty;
        public bool? CurrentIsPublicHospital { get; set; }
        public bool? CurrentInPatient { get; set; }
        public bool? CurrentOutPatient { get; set; }
        public bool? CurrentDental { get; set; }
        public string CurrentSortOrder { get; set; } = string.Empty;
        public bool CurrentIsBlackList { get; set; } = false;
        public string CurrentLanguage { get; set; } = "vn";

    }
}
