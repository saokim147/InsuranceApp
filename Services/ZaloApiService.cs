using System.Net.Http.Headers;

namespace InsuranceWebApp.Services
{
    public class ZaloApiService:IZaloApiService
    {
        private readonly string _endpoint = "https://graph.zalo.me/v2.0/me/info";
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ZaloApiService(HttpClient httpClient, IConfiguration   configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> GetZaloUserInfoAsync(string userAccessToken, string token)
        {
            var _secretKey = _configuration["ZaloSettings:SecretKey"];
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _endpoint);
                request.Headers.Add("access_token", userAccessToken);
                request.Headers.Add("code", token);
                request.Headers.Add("secret_key", _secretKey);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    return errorBody;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
