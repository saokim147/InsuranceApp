using InsuranceWebApp.Data;
using InsuranceWebApp.Repository;
using InsuranceWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Context
        var connectionString = configuration.GetConnectionString("InsuranceConnection")
            ?? throw new InvalidOperationException("Connection string is not found");

        services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(connectionString));

        // MBTiles Provider
        services.AddSingleton(sp =>
        {
            var path = configuration["MBTilesPath"] ?? string.Empty;
            return new MBTileProvider(path);
        });

        // Application Services
        services.AddScoped<IMapService, MapService>();
        services.AddHttpClient<IZaloApiService, ZaloApiService>();
        services.AddScoped<IHospitalRepository, HospitalRepository>();
        services.AddScoped<ExportService>();

        return services;
    }
}
