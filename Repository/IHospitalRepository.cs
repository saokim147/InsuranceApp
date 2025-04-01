using InsuranceWebApp.Helper;
using InsuranceWebApp.Data;
using InsuranceWebApp.Models;
namespace InsuranceWebApp.Repository
{
    public interface IHospitalRepository
    {
        Task InsertAsync(Hospital hospital);
        Task BulkInsertAsync(List<Hospital> hospitals);
        Task<Hospital> GetHospitalByIdAsync(int id);
        Task<List<HospitalDTO>> GetHospitalListAsync(string lang);
        Task<List<string>> FindHospitalNameAsync(HospitalFindCriterias hospitalCreterias);
        Task<CityDTO> GetCityByIdAsync(int? cityId,string lang);
        Task<DistrictDTO> GetDistrictByIdAsync(int? districtId,string lang);
        Task<WardDTO> GetWardByIdAsync(int? wardId,string lang);
        Task<PagedList<Hospital>> GetPagedListAsync(PagingCriteria pagingCreterias, HospitalSortBy sortBy = HospitalSortBy.NameAscending);
        Task<PagedList<HospitalViewModel>> FindAsync(PagingCriteria pagingCreterias, HospitalFindCriterias hospitalCreterias, HospitalSortBy sortBy = HospitalSortBy.NameAscending);
        Task<List<CityDTO>> FindCitiesAsync(string cityName,string lang);
        Task<List<DistrictDTO>> FindDistrictsAsync(string districtName, string cityName,string lang);
        Task<List<WardDTO>> FindWardsAsync(string wardName, string districtName,string lang);
        Task UpdateAsync();
        Task DeleteAsync(int id);
        Task<List<FileResponseDTO>> GetFileResponseDTOAsync(bool isBlackList,string lang);
        Task<int> BulkDeleteAsync(List<int> hospitalIds);
        Task DeleteAllAsync();
    }
}
