using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InsuranceWebApp.Services;
using InsuranceWebApp.Data;
using InsuranceWebApp.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

var app = builder.Build();

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
