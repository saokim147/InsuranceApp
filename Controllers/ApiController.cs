using InsuranceWebApp.Helper;
using InsuranceWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebApp.Controllers
{
    [ApiController]
    [Route("/api")]
    public class InternalApiController : ControllerBase
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly ExportService _exportService;
        public InternalApiController(IHospitalRepository hospitalRepository,ExportService exportService)
        {
            _hospitalRepository = hospitalRepository;
            _exportService = exportService;

        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportFile(string fileType, bool isBlackList = false, string lang = "vi", string cityName = "")
        {
            var data = await _hospitalRepository.GetFileResponseDTOAsync(isBlackList, lang, cityName);
            var id = DateTime.Now.ToString();
            switch (fileType.ToLower())
            {
                case "csv":
                    var csvStream = await _exportService.ExportToCSV(data);
                    return File(csvStream, HttpContentTypeFormat.CSV, $"hospitals-{id}.csv");
                case "excel":
                    var excelStream = _exportService.ExportToExcelWithClosedXML(data);
                    return File(excelStream, HttpContentTypeFormat.EXCEL, $"hospitals-{id}.xlsx");
                default:
                    return BadRequest("Unsupported file type");
            }
        }
    }
    
}
