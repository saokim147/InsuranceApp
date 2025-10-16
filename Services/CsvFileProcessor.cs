using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using InsuranceWebApp.Data;
using InsuranceWebApp.Helper;

namespace InsuranceWebApp.Services
{
    //public class CsvFileProcessor : IFileProcessor
    //{
    //    public string[] SupportedExtensions => ["csv"];

    //    public bool CanProcess(string fileExtension)
    //    {
    //        return SupportedExtensions.Contains(fileExtension);
    //    }

    //    public async Task<MemoryStream> ExportAsync(List<HospitalDTO> hospitals, MemoryStream stream)
    //    {
    //        using var writer = new StreamWriter(stream);
    //        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
    //        csv.WriteRecords(hospitals);
    //        await writer.FlushAsync();
    //        return stream;
    //    }

    //    public Task<List<Hospital>> ProcessFileAsync(Stream fileStream)
    //    {
    //        using var reader=new StreamReader(fileStream);
    //        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
    //        throw new NotImplementedException();
    //    }

    //    public Task<List<Hospital>> ProcessFileAsync(Stream fileStream, bool isBlackList)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
