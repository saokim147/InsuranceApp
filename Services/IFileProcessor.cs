using InsuranceWebApp.Data;

namespace InsuranceWebApp.Services
{
    public interface IFileProcessor
    {
        string[] SupportedExtensions { get; }
        bool CanProcess(string fileExtension);
        Task ExportAsync(List<HospitalDTO> hospitals, MemoryStream stream);
        Task<List<Hospital>> ProcessFileAsync(Stream fileStream,bool isBlackList);
    }
}
