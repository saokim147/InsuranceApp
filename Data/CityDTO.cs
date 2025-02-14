using System.Text.Json.Serialization;

namespace InsuranceWebApp.Data
{
    public class CityDTO
    {
        [JsonPropertyName("cityName")]
        public string CityName { get; set; } = null!;
    }
}

