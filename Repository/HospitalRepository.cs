using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        public HospitalDbContext _context;
        public HospitalRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Hospital>> FindAllAsync()
        {
            return  await _context.Hospitals.ToListAsync();
        }
        public async Task<PagedList<Hospital>> FindAsync(
                PagingCreteria pagingCreterias,
                HospitalFindCreterias hospitalCreterias,
                HospitalSortBy sortBy = HospitalSortBy.Default)
        {
            var hospitals = _context.Hospitals.AsQueryable();

            if (!string.IsNullOrEmpty(hospitalCreterias.HospitalName))
            {
                var hospitalName = hospitalCreterias.HospitalName;
                hospitals = hospitals.Where(h =>
                            EF.Functions.Like(
                                EF.Functions.Collate(h.HospitalName, "SQL_LATIN1_GENERAL_CP1_CI_AI"), $"%{hospitalName}%"));
            }
            if (hospitalCreterias.IsAdvancedSearch)
            {
                if (!string.IsNullOrEmpty(hospitalCreterias.CityName))
                {
                    var cityName = hospitalCreterias.CityName;
                    hospitals = hospitals.Include(h => h.City)
                                         .Where(h => EF.Functions.Collate(h.City.CityName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                            .Contains(cityName));
                }
                if (!string.IsNullOrEmpty(hospitalCreterias.DistrictName))
                {
                    var districtName = hospitalCreterias.DistrictName;
                    hospitals = hospitals.Include(h => h.District)
                                         .Where(h => EF.Functions.Collate(h.District.DistrictName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                         .Contains(districtName));
                }
                if (!string.IsNullOrEmpty(hospitalCreterias.WardName))
                {
                    var wardName = hospitalCreterias.WardName;
                    hospitals = hospitals.Include(h => h.Ward)
                                         .Where(h => EF.Functions.Collate(h.Ward.WardName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                         .Contains(wardName));
                }
                hospitals = hospitals.Where(h => h.IsPublicHospital == hospitalCreterias.IsPublicHospital ||
                                                h.InPatient == hospitalCreterias.InPatient ||
                                                h.OutPatient == hospitalCreterias.OutPatient ||
                                                h.Dental == hospitalCreterias.Dental);
            }
            hospitals = sortBy switch
            {
                HospitalSortBy.NameAscending => hospitals.OrderBy(h => h.HospitalName),
                HospitalSortBy.NameDescending => hospitals.OrderByDescending(h => h.HospitalName),
                _ => hospitals.OrderBy(h => h.HospitalId),
            };
            return await PagedList<Hospital>.CreateAsync(hospitals, pagingCreterias.PageNumber, pagingCreterias.PageSize);
        }

        public async Task<IEnumerable<City>> FindCitiesAsync(string cityName)
        {
            var cities = _context.Cities.AsQueryable();
            if (!string.IsNullOrEmpty(cityName))
            {
                cityName = cityName.RemoveDiacritics().ToLower();
                cities = cities.Where(city =>
                    EF.Functions.Like(
                        EF.Functions.Collate(city.CityName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{cityName}%"));
            }
            return await cities.ToListAsync();
        }

        public async Task<IEnumerable<District>> FindDistrictsAsync(string districtName)
        {
            var districts = _context.Districts.AsQueryable();
            if (!string.IsNullOrEmpty(districtName))
            {
                districtName = districtName.RemoveDiacritics().ToLower();
                districts = districts.Where(district =>
                    EF.Functions.Like(
                        EF.Functions.Collate(district.DistrictName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{districtName}%"));
            }
            return await districts.ToListAsync();
        }

        public async Task<IEnumerable<Ward>> FindWardsAsync(string wardName)
        {
            var wards = _context.Wards.AsQueryable();
            if (!string.IsNullOrEmpty(wardName))
            {
                wardName = wardName.RemoveDiacritics().ToLower();
                wards = wards.Where(ward =>
                    EF.Functions.Like(
                        EF.Functions.Collate(ward.WardName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{wardName.ToLower()}%"));
            }
            return await wards.ToListAsync();
        }
        public async Task<Hospital> GetHospitalByIdAsync(int id)
        {
            return await _context.Hospitals.FindAsync(id);
        }
        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                await _context.SaveChangesAsync();
            }
        }


    }
}
