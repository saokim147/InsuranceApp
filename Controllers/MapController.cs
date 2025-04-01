
using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;
using InsuranceWebApp.Services;
using Microsoft.AspNetCore.Mvc;
namespace InsuranceWebApp.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapService _mapService;
        private readonly ILogger _logger;
        public MapController(IMapService mapService, ILogger<MapController> logger)
        {
            _mapService = mapService;
            _logger = logger;
        }
        // fix the json to return hospitalDTO
        public async Task<IActionResult> Index(double latitude = 106.7035, double longitude = 106.7035, int range = 5,string lang="vi")
        {
            var point = new GeoPoint(latitude, longitude);
            var hospitalViewModels = await _mapService.GetNearByHospitalAsync(point, range,lang);
            if (Request.Headers.ContentType == HttpContentTypeFormat.JSON)
            {
                return Json(hospitalViewModels.ToMapDTO());
            }
            return View(hospitalViewModels);
        }


        [HttpGet("tiles/{z}/{x}/{y}.pbf")]
        public IActionResult GetMapTiles(int z = 5, int x = 31, int y = 31)
        {
            var data = _mapService.GetTiles(z, x, y);
            if (data == null)
            {
                return NotFound("Tile not found");
            }
            Response.Headers.ContentEncoding = "gzip";
            return File(data, HttpContentTypeFormat.PBF, $"{z}.pbf");
        }
        [HttpGet("Map/{fontstack}/{range:regex(^[[\\d]]+-[[\\d]]+$)}.pbf")]
        public async Task<IActionResult> GetFont(string fontstack, string range)
        {
            fontstack = Uri.UnescapeDataString(fontstack);
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "static", "map", "font");
            var fileName = Path.Combine(rootPath, fontstack, $"{range}.pbf");
            _logger.LogInformation("Font file name: {fileName}", fileName);
            if (!System.IO.File.Exists(fileName))
            {
                return NotFound("Font not found");
            }
            var data = await System.IO.File.ReadAllBytesAsync(fileName);
            Response.Headers.LastModified = DateTime.Now.ToString();
            return File(data, HttpContentTypeFormat.PBF, $"{fontstack}-{range}.pbf");
        }
    }
}
