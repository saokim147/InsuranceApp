using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Services
{
    public interface IMapService
    {
        byte[]? GetTiles(int zoom, int x, int y);
        Task<List<HospitalDTO>> GetNearByHospitalAsync(GeoPoint userLocation, int range,string lang);
    }
}
