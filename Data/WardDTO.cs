using System.Text.Json.Serialization;
namespace InsuranceWebApp.Data
{
    public class WardDTO
    {
        [JsonPropertyName("wardName")]
        public string WardName { get; set; } = null!;
    }
}

