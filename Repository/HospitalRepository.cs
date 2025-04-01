using System.Data;
using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;
using InsuranceWebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly HospitalDbContext _context;
        public HospitalRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
        }
        private static DataTable GetHospitalDataTable(List<Hospital> hospitals)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(nameof(Hospital.HospitalName), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.HospitalAddress), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.BillingTime), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.OutPatient), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.InPatient), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.Dental), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.IsPublicHospital), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.InsuranceAndDirectBilling), typeof(string));
            dataTable.Columns.Add(nameof(Hospital.Note), typeof(string));
            foreach (var hospital in hospitals)
            {
                dataTable.Rows.Add(hospital.HospitalName, hospital.HospitalAddress, hospital.BillingTime, hospital.OutPatient, hospital.InPatient, hospital.Dental, hospital.IsPublicHospital, hospital.InsuranceAndDirectBilling, hospital.Note);
            }
            return dataTable;
        }
        public async Task BulkInsertAsync(List<Hospital> hospitals)
        {
            var dataTable = GetHospitalDataTable(hospitals);
            using var bulkCopy = new SqlBulkCopy(_context.Database.GetDbConnection().ConnectionString, SqlBulkCopyOptions.TableLock);
            bulkCopy.DestinationTableName = "Hospitals";
            await bulkCopy.WriteToServerAsync(dataTable);
        }
        // ...

        public async Task<List<HospitalDTO>> GetHospitalListAsync(string lang) => await _context.Hospitals.Select(h => h.ToHospitalDTO(lang)).ToListAsync();
        public async Task<Hospital> GetHospitalByIdAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            return hospital ?? new Hospital();
            }
        public async Task<List<FileResponseDTO>> GetFileResponseDTOAsync(bool isBlackList=false,string lang="vi")
        {
            var hospitals = _context.Hospitals.AsQueryable();
            hospitals = hospitals.Where(h => h.IsBlackList == isBlackList);
            
            return await hospitals
                .Include(h => h.City)
                .Include(h => h.District)
                .Include(h => h.Ward)
                .Select(h => new FileResponseDTO(
                    lang == "en" ? h.HospitalNameEn ?? h.HospitalName : h.HospitalName,
                    lang == "en" ? h.HospitalAddressEn ?? h.HospitalAddress : h.HospitalAddress,
                    h.PhoneNumber,
                    lang == "en" ? h.BillingTimeEn ?? h.BillingTime : h.BillingTime,
                    lang == "en" ? h.InsuranceAndDirectBillingEn ?? h.InsuranceAndDirectBilling : h.InsuranceAndDirectBilling,
                    lang == "en" ? h.NoteEn ?? h.Note : h.Note,
                    h.City != null ? (lang == "en" ? h.City.CityNameEn ?? h.City.CityName : h.City.CityName) : null,
                    h.District != null ? (lang == "en" ? h.District.DistrictNameEn ?? h.District.DistrictName : h.District.DistrictName) : null,
                    h.Ward != null ? (lang == "en" ? h.Ward.WardNameEn ?? h.Ward.WardName : h.Ward.WardName) : null,
                    h.IsPublicHospital,
                    h.InPatient,
                    h.OutPatient,
                    h.Dental
                ))
                .ToListAsync();
        }


        public async Task<PagedList<Hospital>> GetPagedListAsync(PagingCriteria pagingCreterias, HospitalSortBy sortBy = HospitalSortBy.NameAscending)
        {
            var hospitals = _context.Hospitals.AsQueryable();
            return await PagedList<Hospital>.CreateAsync(hospitals, pagingCreterias.PageNumber, pagingCreterias.PageSize);
        }

        public async Task<List<string>> FindHospitalNameAsync(HospitalFindCriterias hospitalCreterias)
        {
            var hospitals = _context.Hospitals.AsQueryable();
            var hospitalNameList = new List<string>();
            if(!string.IsNullOrEmpty(hospitalCreterias.HospitalName))
            {
                hospitals = hospitals.Where(h => EF.Functions.Collate(h.HospitalName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                .Contains(hospitalCreterias.HospitalName));
            }

            if (!string.IsNullOrEmpty(hospitalCreterias.CityName))
            {
                hospitals = hospitals.Include(h => h.City)
                                     .Where(h => h.City != null &&
                                                 EF.Functions.Collate(h.City.CityName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                                 .Contains(hospitalCreterias.CityName));
            }
            if (!string.IsNullOrEmpty(hospitalCreterias.DistrictName))
            {
                hospitals = hospitals.Include(h => h.District)
                                     .Where(h => h.District != null &&
                                                 EF.Functions.Collate(h.District.DistrictName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                                 .Contains(hospitalCreterias.DistrictName));
            }
            if (!string.IsNullOrEmpty(hospitalCreterias.WardName))
            {
                hospitals = hospitals.Include(h => h.Ward)
                                     .Where(h => h.Ward != null &&
                                         EF.Functions.Collate(h.Ward.WardName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                         .Contains(hospitalCreterias.WardName));
            }
            if (hospitalCreterias.IsPublicHospital.HasValue)
            {
                hospitals = hospitals.Where(h => h.IsPublicHospital == hospitalCreterias.IsPublicHospital);
            }
            if (hospitalCreterias.InPatient.HasValue)
            {
                hospitals = hospitals.Where(h => h.InPatient == hospitalCreterias.InPatient);
            }
            if (hospitalCreterias.OutPatient.HasValue)
            {
                hospitals = hospitals.Where(h => h.OutPatient == hospitalCreterias.OutPatient);
            }
            if (hospitalCreterias.Dental.HasValue)
            {
                hospitals = hospitals.Where(h => h.Dental == hospitalCreterias.Dental);
            }
            hospitals = hospitals.Where(h => h.IsBlackList == hospitalCreterias.IsBlackList);
            if (hospitalCreterias.Language == "vi")
            {
                hospitalNameList = await hospitals.Select(h => h.HospitalName).ToListAsync();
            }
            else
            {
                // fall back to vietnamese if english is not available
                hospitalNameList = await hospitals.Select(h => h.HospitalNameEn ?? h.HospitalName).ToListAsync();
            }
            return hospitalNameList;
        }

        public async Task<PagedList<HospitalViewModel>> FindAsync(
                PagingCriteria pagingCreterias,
                HospitalFindCriterias hospitalCreterias,
                HospitalSortBy sortBy = HospitalSortBy.Default)
        {
            var hospitals = _context.Hospitals.AsQueryable();
            if (!string.IsNullOrEmpty(hospitalCreterias.HospitalName))
            {
                hospitals = hospitals.Where(h => EF.Functions.Collate(h.HospitalName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                .Contains(hospitalCreterias.HospitalName));
            }
            if (!string.IsNullOrEmpty(hospitalCreterias.CityName))
            {
                hospitals = hospitals.Include(h => h.City)
                                     .Where(h => h.City != null &&
                                                 EF.Functions.Collate(h.City.CityName , "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                                 .Contains(hospitalCreterias.CityName));
            }
            if (!string.IsNullOrEmpty(hospitalCreterias.DistrictName))
            {
                hospitals = hospitals.Include(h => h.District)
                                     .Where(h => h.District != null &&
                                                 EF.Functions.Collate(h.District.DistrictName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                                 .Contains(hospitalCreterias.DistrictName));
            }
            if (!string.IsNullOrEmpty(hospitalCreterias.WardName))
            {
                hospitals = hospitals.Include(h => h.Ward)
                                     .Where(h => h.Ward != null &&
                                         EF.Functions.Collate(h.Ward.WardName, "SQL_LATIN1_GENERAL_CP1_CI_AI")
                                         .Contains(hospitalCreterias.WardName));
            }
            if (hospitalCreterias.IsPublicHospital.HasValue)
            {
                hospitals = hospitals.Where(h => h.IsPublicHospital == hospitalCreterias.IsPublicHospital);
            }
            if (hospitalCreterias.InPatient.HasValue)
            {
                hospitals = hospitals.Where(h => h.InPatient == hospitalCreterias.InPatient);
            }
            if (hospitalCreterias.OutPatient.HasValue)
            {
                hospitals = hospitals.Where(h => h.OutPatient == hospitalCreterias.OutPatient);
            }
            if (hospitalCreterias.Dental.HasValue)
            {
                hospitals = hospitals.Where(h => h.Dental == hospitalCreterias.Dental);
            }
            hospitals = hospitals.Where(h => h.IsBlackList == hospitalCreterias.IsBlackList);
            hospitals = sortBy switch
            {
                HospitalSortBy.NameAscending => hospitals.OrderBy(h => h.HospitalName),
                HospitalSortBy.NameDescending => hospitals.OrderByDescending(h => h.HospitalName),
                HospitalSortBy.CityAscending => hospitals.OrderBy(h => h.City != null ? h.City.CityName : string.Empty),
                HospitalSortBy.CityDescending => hospitals.OrderByDescending(h => h.City != null ? h.City.CityName : string.Empty),
                _ => hospitals.OrderBy(h => h.HospitalId),
            };
            var viewModelList = hospitals.Select(h => h.ToHospitalViewModel(hospitalCreterias.Language));
            return await PagedList<HospitalViewModel>.CreateAsync(viewModelList, pagingCreterias.PageNumber, pagingCreterias.PageSize);
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


        public async Task<int> BulkDeleteAsync(List<int> hospitalIds)
        {
            var totalRecordDeleted = await _context.Hospitals
               .Where(h => hospitalIds.Contains(h.HospitalId))
               .ExecuteDeleteAsync();
            return totalRecordDeleted;
        }

        public async Task DeleteAllAsync()
        {
            await _context.Hospitals.ExecuteDeleteAsync();
        }

        public async Task<List<CityDTO>> FindCitiesAsync(string cityName, string lang)
        {
            var cities = _context.Cities.AsQueryable();
            if (!string.IsNullOrEmpty(cityName))
            {
                cityName = cityName.RemoveDiacritics().ToLower();
                cities = cities.Where(city => city != null &&
                    EF.Functions.Like(
                        EF.Functions.Collate(city.CityName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{cityName}%"));
            }
            return await cities.Select(c => c.ToCityDTO(lang))
                .ToListAsync();
        }
        public async Task<List<DistrictDTO>> FindDistrictsAsync(string districtName, string cityName, string lang)
        {
            var districts = _context.Districts.AsQueryable();
            if (!string.IsNullOrEmpty(cityName))
            {
                cityName = cityName.RemoveDiacritics().ToLower();
                districts = districts.Where(district => district.City != null &&
                    EF.Functions.Like(
                        EF.Functions.Collate(district.City.CityName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{cityName}%"));
            }
            if (!string.IsNullOrEmpty(districtName))
            {
                districtName = districtName.RemoveDiacritics().ToLower();
                districts = districts.Where(district =>
                    EF.Functions.Like(
                        EF.Functions.Collate(district.DistrictName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{districtName}%"));
            }
            return await districts.Select(d => d.ToDistrictDTO(lang))
                .ToListAsync();
        }

        public async Task<List<WardDTO>> FindWardsAsync(string wardName, string districtName, string lang)
        {
            var wards = _context.Wards.AsQueryable();
            if (!string.IsNullOrEmpty(districtName))
            {
                districtName = districtName.RemoveDiacritics().ToLower();
                wards = wards.Where(ward => ward.District != null &&
                    EF.Functions.Like(
                        EF.Functions.Collate(ward.District.DistrictName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{districtName}%"));
            }

            if (!string.IsNullOrEmpty(wardName))
            {
                wardName = wardName.RemoveDiacritics().ToLower();
                wards = wards.Where(ward =>
                    EF.Functions.Like(
                        EF.Functions.Collate(ward.WardName.ToLower(), "SQL_LATIN1_GENERAL_CP1_CI_AI"),
                        $"%{wardName}%"));
            }
            return await wards.Select(w => w.ToWardDTO(lang))
                .ToListAsync();
        }

        public async Task<CityDTO?> GetCityByIdAsync(int? cityId, string lang)
        {
            if (!cityId.HasValue)
            {
                return null;
            }

            var city = await _context.Cities
                .Where(c => c.CityId == cityId.Value) 
                .Select(c => c.ToCityDTO(lang)) 
                .FirstOrDefaultAsync();
            return city;
        }

        public async Task<DistrictDTO> GetDistrictByIdAsync(int ?districtId,string lang)
        {
            if(!districtId.HasValue)
            {
                return null;
            }
            var district = await _context.Districts
                .Where(d => d.DistrictId == districtId.Value)
                .Select(d => d.ToDistrictDTO(lang))
                .FirstOrDefaultAsync();
            return district;
        }
        public async Task<WardDTO> GetWardByIdAsync(int ?wardId,string lang)
        {
            if(!wardId.HasValue)
            {
                return null;
            }
            var ward = await _context.Wards
                .Where(w => w.WardId == wardId.Value)
                .Select(w => w.ToWardDTO(lang))
                .FirstOrDefaultAsync();
            return ward;
        }
    }
}
