namespace InsuranceWebApp.Services
{
    //public class FileProcessorFactory
    //{
    //    private readonly IEnumerable<IFileProcessor> _processors;
    //    public FileProcessorFactory(IEnumerable<IFileProcessor> processors)
    //    {
    //        _processors = processors;
    //    }
    //    public IFileProcessor GetProcessor(string fileExtension)
    //    {
    //        var processor = _processors.FirstOrDefault(p => p.CanProcess(fileExtension)) ?? 
    //            throw new NotSupportedException($"Files with extension '{fileExtension}' are not supported.");
    //        return processor;
    //    }
    //    public string GetSupportedFormats()
    //    {
    //        return string.Join(", ", _processors.SelectMany(p => p.SupportedExtensions));
    //    }
    //}
}