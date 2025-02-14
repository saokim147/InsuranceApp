using InsuranceWebApp.Helper;
using InsuranceWebApp.Data;

namespace InsuranceWebApp.Repository
{
    public interface IHospitalRepository
    {
        Task AddAsync(Hospital hospital);
        Task<IEnumerable<Hospital>> FindAllAsync();
        Task<PagedList<Hospital>> FindAsync(PagingCreteria pagingCreterias, HospitalFindCreterias hospitalCreterias, HospitalSortBy sortBy = HospitalSortBy.NameAscending);
        Task<IEnumerable<City>> FindCitiesAsync(string cityName);
        Task<IEnumerable<District>> FindDistrictsAsync(string districtName);
        Task<IEnumerable<Ward>> FindWardsAsync(string wardName);
        Task<Hospital> GetHospitalByIdAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync(int id);

    }
}
