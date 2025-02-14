using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Services
{
    public interface IMapService
    {
        byte[]? GetTiles(int zoom, int x, int y);
        Task<List<HospitalViewModel>> GetNearByHospitalAsync(GeoPoint userLocation, int range);
    }
}
