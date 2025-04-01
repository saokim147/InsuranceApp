using InsuranceWebApp.Models;
namespace InsuranceWebApp.Data
{
    public sealed record class InsuranceDTO(IEnumerable<HospitalViewModel> Data,int TotalRecord);
}
