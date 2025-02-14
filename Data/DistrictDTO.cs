using System.Text.Json.Serialization;

namespace InsuranceWebApp.Data
{
    public class DistrictDTO
    {
        [JsonPropertyName("districtName")]
        public string DistrictName { get; set; } = null!;
    }
}

