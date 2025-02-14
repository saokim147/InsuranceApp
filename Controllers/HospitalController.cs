
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Globalization;

using InsuranceWebApp.Helper;
using InsuranceWebApp.Repository;
using InsuranceWebApp.Services;
using InsuranceWebApp.Models;
using InsuranceWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Controllers
{
    public class HospitalController : Controller
    {
        private readonly ILogger<HospitalController> _logger;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapService _mapService;
        public HospitalController(ILogger<HospitalController> logger, IHospitalRepository hospitalRepository, IMapService mapService)
        {
            _logger = logger;
            _hospitalRepository = hospitalRepository;
            _mapService = mapService;
        }
        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 5,
            string hospitalName = "",
            string cityName = "",
            string districtName = "",
            string wardName = "",
            bool IsAdvancedSearch = false,
            bool IsPublicHospital = true,
            bool InPatient = true,
            bool OutPatient = true,
            bool Dental = true,
            string sortOrder = "")
        {
            var pagingCreteria = new PagingCreteria
            {
                PageNumber = page,
                PageSize = pageSize
            };
            var hospitalFindCreterias = new HospitalFindCreterias
            {
                HospitalName = hospitalName,
                CityName = cityName,
                DistrictName = districtName,
                WardName = wardName,
                IsAdvancedSearch = IsAdvancedSearch,
                IsPublicHospital = IsPublicHospital,
                InPatient = InPatient,
                OutPatient = OutPatient,
                Dental = Dental
            };
            ViewBag.sortOrder = sortOrder;
            ViewBag.CurrentPage = page;
            var hospitalSortBy = HospitalSortBy.Default;
            if (sortOrder == "asc")
            {
                hospitalSortBy = HospitalSortBy.NameAscending;
            }
            if (sortOrder == "desc")
            {
                hospitalSortBy = HospitalSortBy.NameDescending;
            }
            var hospitalPagedList = await _hospitalRepository.FindAsync(pagingCreteria, hospitalFindCreterias, hospitalSortBy);
            var insuranceViewModel = new InsuranceViewModel
            {
                HospitalPageList = hospitalPagedList.Items.Select(h => h.ToHospitalViewModel()),
                CurrentPage = hospitalPagedList.Page,
                TotalRecord = hospitalPagedList.TotalCount,
            };
            if (Request.Headers.ContentType == HttpContentTypeFormat.JSON)
            {
                return Json(insuranceViewModel);
            }
            if (Request.Headers["HX-Request"] == "true")
            {
                return PartialView("_HospitalList", insuranceViewModel);
            }
            return View(insuranceViewModel);
        }
        public async Task<IActionResult> Export(string fileType)
        {
            var hospitals = await _hospitalRepository.FindAllAsync();
            var hospitalViewModels = hospitals.Select(h => h.ToHospitalViewModel());
            if (fileType == "csv")
            {
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream);
                var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(hospitalViewModels);
                return File(memoryStream.ToArray(), HttpContentTypeFormat.CSV, "Hospitals.csv");
            }
            if (fileType == "excel")
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var package = new ExcelPackage();
                var sheet = package.Workbook.Worksheets.Add("Hospital");
                sheet.Cells[1, 1].Value = "Cơ sở y tế";
                sheet.Cells[1, 2].Value = "Địa chỉ";
                sheet.Cells[1, 3].Value = "Số điện thoại";
                sheet.Cells[1, 4].Value = "Thời gian tiếp nhận bảo lãnh cơ sở y tế";
                sheet.Cells[1, 5].Value = "Bệnh nhân nội trú";
                sheet.Cells[1, 6].Value = "Bệnh nhân ngoại trú";
                sheet.Cells[1, 7].Value = "Nha khoa";
                sheet.Cells[1, 8].Value = "Bảo hiểm và thanh toán trực tiếp";
                sheet.Cells[1, 9].Value = "Ghi chú";
                sheet.Cells["A2"].LoadFromCollection(hospitalViewModels, PrintHeaders: false);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                return File(package.GetAsByteArray(), HttpContentTypeFormat.EXCEL, "Hospitals.xlsx");
            }
            // PDF
            QuestPDF.Settings.License = LicenseType.Community;
            var hospitalDocument = new HospitalDocument(hospitalViewModels.ToList());
            var stream = new MemoryStream();
            hospitalDocument.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream, HttpContentTypeFormat.PDF, "Hospitals.pdf");

        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]
        public IActionResult Create()
        {
            if (Request.Headers["HX-Request"] == "true")
            {
                return PartialView();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HospitalViewModel model)
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
                    };
                    await _hospitalRepository.AddAsync(hospital);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                     "see your system administrator.");
            }
            return View(model);
        }

        public async Task<JsonResult> ListCities(string cityName = "")
        {
            var cities = await _hospitalRepository.FindCitiesAsync(cityName);
            var cityDTO = cities.Select(city => city.ToCityDTO());
            return new JsonResult(cityDTO);
        }
        public async Task<JsonResult> ListDistricts(string districtName = "")
        {
            var districts = await _hospitalRepository.FindDistrictsAsync(districtName);
            var districtDTO = districts.Select(districts => districts.ToDistrictDTO());
            return new JsonResult(districtDTO);
        }
        public async Task<JsonResult> ListWards(string wardName = "")
        {
            var wards = await _hospitalRepository.FindWardsAsync(wardName);
            var wardDTO = wards.Select(ward => ward.ToWardDTO());
            return new JsonResult(wardDTO);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _hospitalRepository.GetHospitalByIdAsync(id);
            if (hospital == null)
            {
                return View("Error");
            }
            return View(hospital.ToEditHospitalViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(EditHospitalViewModel model)
        {
            var hospital = await _hospitalRepository.GetHospitalByIdAsync(model.HospitalId);
            if (hospital != null)
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
                await _hospitalRepository.UpdateAsync();
                return RedirectToAction("Index", "Hospital");
            }
            ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _hospitalRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
