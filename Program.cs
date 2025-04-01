using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InsuranceWebApp.Services;
using InsuranceWebApp.Data;
using InsuranceWebApp.Repository;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var connectionString = builder.Configuration.GetConnectionString("InsuranceConnection")
    ?? throw new InvalidOperationException("Connection string is not found");


builder.Services.AddDbContext<HospitalDbContext>(options =>
   options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<HospitalDbContext>()
.AddDefaultTokenProviders();




builder.Services.AddSingleton(
    sp =>
    {
        var path = builder.Configuration["MBTilesPath"] ?? string.Empty;
        return new MBTileProvider(path);
    }
 );

builder.Services.AddScoped<IMapService, MapService>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<ExportService>();
//builder.Services.AddScoped<IFileProcessor, CsvFileProcessor>();
/*builder.Services.AddScoped<IFileProcessor, ExcelFileProcessor>();
builder.Services.AddScoped<FileProcessorFactory>();*/


var supportedCultures = new[] { "vi", "en" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
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


var app = builder.Build();
app.UseRequestLocalization();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hospital}/{action=Index}/{id?}");



app.Run();
