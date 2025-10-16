using System.Text.Json;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;
using InsuranceWebApp.Repository;
namespace InsuranceWebApp.Services
{
    public class MapService : IMapService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly MBTileProvider _mbtileProvider;
        private string baseUrl = "http://localhost:8080/search.php";
        public MapService(IHospitalRepository hospitalRepository, MBTileProvider mtbileProvider)
        {
            _hospitalRepository = hospitalRepository;
            _mbtileProvider = mtbileProvider;
        }
        private string BuildUrl(string query)
        {
            var parameters = new Dictionary<string, string>
            {
                { "q", query }
            };
            var uriBuilder = new UriBuilder(baseUrl)
            {
                Query = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"))
            };
            return uriBuilder.ToString();
        }
        private async Task<string> GetAddressAsync(string address)
        {
            var url = BuildUrl(address);
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        private async Task<MapApiResponseDTO> ExtractData(string address)
        {
            var response = await GetAddressAsync(address);
            var items = JsonSerializer.Deserialize<List<MapApiResponseDTO>>(response);
            return items.FirstOrDefault();
        }



        public async Task<List<HospitalDTO>> GetNearByHospitalAsync(GeoPoint userLocation, int range = 5, string lang = "vi")
        {
            var hospitals = await _hospitalRepository.GetHospitalListAsync(lang);
            return userLocation.FindNearByLocation(hospitals, range);
        }


        public byte[]? GetTiles(int zoom, int x, int y)
        {
            return _mbtileProvider.GetTiles(zoom, x, y);
        }

        public async Task UpdateLocationAsync(string lang)
        {
            var hospitals = await _hospitalRepository.GetHospitalListAsync(lang);
            foreach (var hospital in hospitals)
            {
                var location = await ExtractData(hospital.HospitalAddress);
                hospital.Latitude = double.Parse(location.lat);
                hospital.Longitude = double.Parse(location.lon);
            }
        }
    }
}
