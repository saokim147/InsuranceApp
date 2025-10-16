using InsuranceWebApp.Data;
using ClosedXML.Excel;
namespace InsuranceWebApp.Services
{
    //public class ExcelFileProcessor : IFileProcessor
    //{
    //    public string[] SupportedExtensions => ["xls", "xlsx"];
    //    public bool CanProcess(string fileExtension)
    //    {
    //        return SupportedExtensions.Contains(fileExtension);
    //    }

    //    public Task<List<Hospital>> ProcessFileAsync(Stream fileStream,bool isBlackList=false)
    //    {
    //        var hospitals=new List<Hospital>();
    //        using var workbook = new XLWorkbook(fileStream);
    //        var worksheet = workbook.Worksheet(1);
    //        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
    //        foreach(var row in rows)
    //        {
    //            var hospital = new Hospital
    //            {
    //                HospitalName = row.Cell(1).Value.ToString(),
    //                HospitalAddress = row.Cell(2).Value.ToString(),
    //                IsPublicHospital = row.Cell(3).Value.ToString().ToLower() == "x",
    //                InPatient = row.Cell(4).Value.ToString().ToLower() == "x",
    //                OutPatient = row.Cell(5).Value.ToString().ToLower() == "x",
    //                Dental = row.Cell(6).Value.ToString().ToLower() == "x",
    //                PhoneNumber = row.Cell(7).Value.ToString(),
    //                BillingTime = row.Cell(8).Value.ToString(),
    //                InsuranceAndDirectBilling = row.Cell(9).Value.ToString(),
    //                Note = row.Cell(10).Value.ToString(),
    //                IsBlackList = isBlackList,
    //                Longitude=0,
    //                Latitude = 0
    //            };
    //            hospitals.Add(hospital);
    //        }

    //        throw new NotImplementedException();
    //    }
    //}
}
