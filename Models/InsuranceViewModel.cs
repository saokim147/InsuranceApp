namespace InsuranceWebApp.Models
{
    public class InsuranceViewModel
    {
        public IEnumerable<HospitalViewModel> HospitalPageList { get; set; } = [];
        public int TotalRecord { get; set; }
    
        public int CurrentPage { get; set; }
    }
}
