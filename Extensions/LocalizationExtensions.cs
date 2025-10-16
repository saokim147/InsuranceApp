using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace InsuranceWebApp.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddApplicationLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddControllersWithViews()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

        var supportedCultures = new[] { "vi", "en" };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();

            options.DefaultRequestCulture = new RequestCulture("vi");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;
            options.RequestCultureProviders =
            [
                new QueryStringRequestCultureProvider
                {
                    QueryStringKey = "lang",
                    UIQueryStringKey = "lang"
                },
            ];
        });

        return services;
    }
}
