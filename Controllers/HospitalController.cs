using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InsuranceWebApp.Helper;
using InsuranceWebApp.Repository;
using InsuranceWebApp.Models;
using InsuranceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Htmx;
namespace InsuranceWebApp.Controllers
{
    public class HospitalController : Controller
    {
        private readonly ILogger<HospitalController> _logger;
        private readonly ExportService _exportService;
        private readonly IHospitalRepository _hospitalRepository;
        public HospitalController(
                ILogger<HospitalController> logger,
                IHospitalRepository hospitalRepository,
                ExportService exportService)
        {
            _logger = logger;
            _hospitalRepository = hospitalRepository;
            _exportService = exportService;
        }

        public async Task<IActionResult> Search(
            string hospitalName = "",
            string cityName = "",
            string districtName = "",
            string wardName = "",
            bool? IsPublicHospital = null,
            bool? InPatient = null,
            bool? OutPatient = null,
            bool? Dental = null,
            bool isBlackList = false,
            string lang = "vi")
        {
            var hospitalFindCriterias = new HospitalFindCriterias
            {
                HospitalName = hospitalName,
                CityName = cityName,
                DistrictName = districtName,
                WardName = wardName,
                IsPublicHospital = IsPublicHospital,
                InPatient = InPatient,
                OutPatient = OutPatient,
                Dental = Dental,
                IsBlackList = isBlackList,
                Language = lang
            };
            var hospitalNameList = await _hospitalRepository.FindHospitalNameAsync(hospitalFindCriterias);
            var searchViewModelList = hospitalNameList.ToSearchViewModelList();
            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("_SearchList", searchViewModelList);
            }
            if (Request.Headers.ContentType == HttpContentTypeFormat.JSON)
            {
                return Json(hospitalNameList.toSearchDTO());
            }
            return View("_SearchList", searchViewModelList);
        }


        public IActionResult Check()
        {
            // Response.Headers.AccessControlAllowOrigin = "https://localhost:2999";
            // Response.Headers.AccessControlAllowMethods = "*";
            // Response.Headers.AccessControlAllowHeaders = "*";
            return Json("OK");
        }


        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 5,
            string hospitalName = "",
            string cityName = "",
            string districtName = "",
            string wardName = "",
            bool? isPublicHospital = null,
            bool? inPatient = null,
            bool? outPatient = null,
            bool? dental = null,
            string sortOrder = "default",
            bool isBlackList = false,
            string lang = "vi")
        {
            var pagingCriteria = new PagingCriteria
            {
                PageNumber = page,
                PageSize = pageSize
            };
            var hospitalFindCriterias = new HospitalFindCriterias
            {
                HospitalName = hospitalName,
                CityName = cityName,
                DistrictName = districtName,
                WardName = wardName,
                IsPublicHospital = isPublicHospital,
                InPatient = inPatient,
                OutPatient = outPatient,
                Dental = dental,
                IsBlackList = isBlackList,
                Language = lang
            };
            var hospitalSortBy = HospitalSortBy.Default;
            if (sortOrder == "asc")
            {
                hospitalSortBy = HospitalSortBy.NameAscending;
            }
            if (sortOrder == "desc")
            {
                hospitalSortBy = HospitalSortBy.NameDescending;
            }
            var hospitalPagedList = await _hospitalRepository.FindAsync(pagingCriteria, hospitalFindCriterias, hospitalSortBy);
            var insuranceViewModel = hospitalPagedList.ToInsuranceViewModel(hospitalName, cityName,
                    districtName,
                    wardName,
                    isPublicHospital,
                    inPatient,
                    outPatient,
                    dental,
                    sortOrder,
                    isBlackList,
                    lang);
      
            if (Request.Headers.ContentType == HttpContentTypeFormat.JSON)
            {
                Response.Headers.AccessControlAllowOrigin = "*";
                Response.Headers.AccessControlAllowMethods = "*";
                Response.Headers.AccessControlAllowHeaders = "*";
                return Json(hospitalPagedList.ToInsuranceDTO());
            }
            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("_HospitalList", insuranceViewModel);
            }
            return View(insuranceViewModel);
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHospitalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hospital = new Hospital
                    {
                        HospitalName = model.HospitalName,
                        IsPublicHospital = model.IsPublicHospital,
                        HospitalAddress = model.HospitalAddress,
                        InPatient = model.InPatient,
                        OutPatient = model.OutPatient,
                        Dental = model.Dental,
                        PhoneNumber = model.PhoneNumber,
                        BillingTime = model.BillingTime,
                        InsuranceAndDirectBilling = model.InsuranceAndDirectBilling,
                        Note = model.Note,
                        Latitude = 0,
                        Longitude = 0,
                        IsBlackList = model.IsBlackList,
                        CityId = model.CityId,
                        DistrictId = model.DistrictId,
                        WardId = model.WardId
                    };
                    await _hospitalRepository.InsertAsync(hospital);
                    Response.Htmx(h =>
                    {
                        h.WithTrigger(EventMessage.SUCCESS_CREATE_MSG);
                    });
                    var pagingCreterias = new PagingCriteria
                    {
                        PageNumber = 1,
                        PageSize = 5
                    };
                    var hospitalCreterias = new HospitalFindCriterias
                    {

                    };
                    var hospitalPagedList = await _hospitalRepository.FindAsync(pagingCreterias, hospitalCreterias, HospitalSortBy.Default);
                    return PartialView("_HospitalList", hospitalPagedList.ToInsuranceViewModel());
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                     "see your system administrator.");
            }
            Response.Htmx(h =>
            {
                h.WithTrigger(EventMessage.FAILED_CREATE_MSG);
                h.Retarget("#create-form");
                h.Reswap("outerHTML");
            });
            return PartialView("Create", model);
        }

        public async Task<IActionResult> ListCities(string cityName = "", string lang = "vi")
        {
            var cities = await _hospitalRepository.FindCitiesAsync(cityName.Trim(), lang);

            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("_CityList", cities);
            }
            return Json(new { data = cities });
        }
        public async Task<IActionResult> ListDistricts(string districtName = "", string cityName = "", string lang = "vi")
        {
            var districts = await _hospitalRepository.FindDistrictsAsync(districtName.Trim(), cityName.Trim(), lang);
            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("_DistrictList", districts);
            }
            return Json(new { data = districts });
        }

        public async Task<IActionResult> ListWards(string wardName = "", string districtName = "", string lang = "vi")
        {
            var wards = await _hospitalRepository.FindWardsAsync(wardName.Trim(), districtName.Trim(), lang);
            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("_WardList", wards);
            }
            return Json(new { data = wards });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id, string lang = "vi", int currentPage = 1)
        {
            var hospital = await _hospitalRepository.GetHospitalByIdAsync(id);
            if (hospital == null)
            {
                return View("Error");
            }
            var city = await _hospitalRepository.GetCityByIdAsync(hospital.CityId, lang);
            var district = await _hospitalRepository.GetDistrictByIdAsync(hospital.DistrictId, lang);
            var ward = await _hospitalRepository.GetWardByIdAsync(hospital.WardId, lang);
            var editViewModel = hospital.ToEditHospitalViewModel(
                    lang,
                    city?.CityName ?? string.Empty,
                    district?.DistrictName ?? string.Empty,
                    ward?.WardName ?? string.Empty,
                    currentPage
            );
            if (Request.IsHtmx())
            {
                Response.Headers.Append("Vary", "HX-Request");
                return PartialView("Edit", editViewModel);
            }
            return View("_BeforeEdit", editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(EditHospitalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", model);
            }
            var hospital = await _hospitalRepository.GetHospitalByIdAsync(model.HospitalId);
            if (hospital != null)
            {
                UpdateHospitalFromEditModel(hospital, model);
                await _hospitalRepository.UpdateAsync();
                var pagingCreterias = new PagingCriteria
                {
                    PageNumber = model.CurrentPage,
                    PageSize = 5
                };
                var hospitalCreterias = new HospitalFindCriterias
                {
                    IsBlackList = model.IsBlackList
                };
                var hospitalPagedList = await _hospitalRepository.FindAsync(pagingCreterias, hospitalCreterias, HospitalSortBy.Default);
                Response.Headers["HX-Trigger"] = EventMessage.SUCCESS_UPDATE_MSG;
                if (Request.IsHtmx())
                {
                    Response.Headers.Append("Vary", "HX-Request");
                    return PartialView("_HospitalList", hospitalPagedList.ToInsuranceViewModel());
                }
                return View("Edit");
            }
            Response.Htmx(h =>
                h.WithTrigger(EventMessage.FAILED_UPDATE_MSG)
                 .Retarget("#edit-form")
                 .Reswap("outerHTML"));
            return PartialView("Edit", model);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            await _hospitalRepository.BulkDeleteAsync(model.DeletedIds);
            var hospitalCriterias = new HospitalFindCriterias { };
            var pagingCriteria = new PagingCriteria { };
            var hospitalPagedList = await _hospitalRepository.FindAsync(pagingCriteria, hospitalCriterias, HospitalSortBy.Default);
            Response.Headers["HX-Trigger"] = EventMessage.SUCCESS_DELETE_MSG;
            return PartialView("_HospitalList", hospitalPagedList.ToInsuranceViewModel());
        }
        [Authorize]
        public IActionResult UploadForm()
        {
            Response.Headers.Append("Vary", "HX-Request");
            return PartialView();
        }
        public async Task<IActionResult> Export(string fileType, bool isBlackList = false, string lang = "vi", string cityName = "")
        {
            var data = await _hospitalRepository.GetFileResponseDTOAsync(isBlackList, lang, cityName);
            Response.Htmx(h =>
                h.Redirect(Url.Action("Export", new { fileType, isBlackList, lang, cityName }))
            );
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
        private static void UpdateHospitalFromEditModel(Hospital hospital, EditHospitalViewModel model)
        {
            hospital.HospitalAddress = model.HospitalAddress;
            hospital.HospitalName = model.HospitalName;
            hospital.PhoneNumber = model.PhoneNumber;
            hospital.IsPublicHospital = model.IsPublicHospital;
            hospital.InPatient = model.InPatient;
            hospital.Dental = model.Dental;
            hospital.InsuranceAndDirectBilling = model.InsuranceAndDirectBilling;
            hospital.OutPatient = model.OutPatient;
            hospital.BillingTime = model.BillingTime;
            hospital.Note = model.Note;
            hospital.IsBlackList = model.IsBlackList;
            hospital.CityId = model.CityId;
            hospital.DistrictId = model.DistrictId;
            hospital.WardId = model.WardId;
        }
        public IActionResult Testing()
        {
            return Json("qwe");
        }
    }
}
