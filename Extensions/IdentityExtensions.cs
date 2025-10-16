using InsuranceWebApp.Data;
using Microsoft.AspNetCore.Identity;

namespace InsuranceWebApp.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<HospitalDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
