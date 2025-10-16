namespace InsuranceWebApp.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureMiddlewarePipeline(this WebApplication app)
    {
        // Localization
        app.UseRequestLocalization();

        // Exception Handling and Security
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // HTTPS Redirection
        app.UseHttpsRedirection();

        // Routing
        app.UseRouting();

        // CORS - Note: Consider restricting this in production
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true));

        // Static Files
        app.UseStaticFiles();

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Route Mapping
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Hospital}/{action=Index}/{id?}");

        return app;
    }
}
