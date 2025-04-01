using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;
using InsuranceWebApp.Models;
using InsuranceWebApp.Repository;
namespace InsuranceWebApp.Services
{
    public class MapService : IMapService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly MBTileProvider _mbtileProvider;
        public MapService(IHospitalRepository hospitalRepository, MBTileProvider mtbileProvider)
        {
            _hospitalRepository = hospitalRepository;
            _mbtileProvider = mtbileProvider;
        }

        public async Task<List<HospitalDTO>> GetNearByHospitalAsync(GeoPoint userLocation, int range = 5, string lang = "vn")
        {
            var hospitals = await _hospitalRepository.GetHospitalListAsync(lang);
            return userLocation.FindNearByLocation(hospitals, range);
        }

        public byte[]? GetTiles(int zoom, int x, int y)
        {
            return _mbtileProvider.GetTiles(zoom, x, y);
        }
       

    }
}
