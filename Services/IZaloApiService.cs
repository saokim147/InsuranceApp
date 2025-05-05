namespace InsuranceWebApp.Services
{
    public interface IZaloApiService
    {
        Task<string> GetZaloUserInfoAsync(string userAccessToken, string token);

    }
}
