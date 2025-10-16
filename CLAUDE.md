# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

InsuranceWebApp is an ASP.NET Core 8.0 MVC application for managing hospital and insurance information. The application supports bilingual content (Vietnamese and English), integrates with map services for geocoding, and provides CRUD operations for hospital data with import/export capabilities.

## Build and Development Commands

### Build and Run
```bash
# Build the project (automatically runs Tailwind CSS build via the Tailwind target)
dotnet build

# Run the application
dotnet run

# Watch mode for development
dotnet watch run
```

### CSS/Tailwind
```bash
# Build CSS (production/minified)
npm run css:build

# Watch mode for CSS development
npm run tailwind
```

### Database Migrations
```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## Architecture Overview

### Core Technologies
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core with SQL Server
- **Frontend**: Razor Views with HTMX for dynamic updates, Tailwind CSS for styling
- **Authentication**: ASP.NET Core Identity with custom ApplicationUser
- **Localization**: Built-in .NET localization with Vietnamese (default) and English support

### Key Architectural Patterns

**Repository Pattern**: The application uses `IHospitalRepository` to abstract data access logic. All database operations go through this repository interface.

**Service Layer**:
- `IMapService/MapService`: Handles geocoding and nearby hospital search using an external Nominatim instance (localhost:8080/search.php) and MBTiles for map tiles
- `IZaloApiService/ZaloApiService`: Integrates with Zalo API for user authentication
- `ExportService`: Handles CSV/Excel export using ClosedXML and CsvHelper
- `IFileProcessor`: Factory pattern for processing Excel/CSV file uploads

**Data Model Hierarchy**:
- `City` → `District` → `Ward` → `Hospital`
- Each entity has bilingual fields (e.g., `HospitalName` and `HospitalNameEn`)
- DTOs are used for data transfer (CityDTO, DistrictDTO, WardDTO, HospitalDTO, etc.)

**HTMX Integration**: Controllers return partial views when `Request.IsHtmx()` is true, enabling seamless dynamic updates without full page reloads. The application uses custom HTMX triggers defined in `Helper/EventMessage.cs` for success/failure notifications.

**Custom Tag Helpers**: The application includes custom tag helpers in the `TagHelpers/` directory for pagination, buttons, checkboxes, selects, and search boxes that integrate with the application's styling and behavior.

### Configuration Notes

**Connection Strings**: Defined in `appsettings.json` under `ConnectionStrings:InsuranceConnection`

**External Services**:
- MBTiles path configured via `MBTilesPath` in appsettings
- Zalo API secret key in `ZaloSettings:SecretKey` (should not be committed)
- MapService uses localhost:8080 Nominatim instance (see Services/MapService.cs:12)

**Localization**: Resources stored in `Resources/` directory with `.resx` files. Default culture is Vietnamese ("vi"), with English ("en") support. Language can be switched via `?lang=en` or `?lang=vi` query parameter.

**Identity Configuration**: Password requirements set in Program.cs (line 23-32) - requires digit, lowercase, uppercase, non-alphanumeric, min length 6.

### Important Implementation Details

**Build Process**: The `.csproj` file includes a custom `Tailwind` target that runs before build, executing `npm run css:build` to compile Tailwind CSS automatically.

**CORS**: Configured to allow any origin/method/header with credentials (Program.cs:84-88) - consider restricting in production.

**Default Route**: Application defaults to `Hospital/Index` (not Home) as the landing page.

**Authorization**: Create, Edit, Delete, and Upload operations require `[Authorize]` attribute. Privacy page also requires authentication.

**Hospital Filtering**: The `HospitalFindCriterias` class supports filtering by name, location (city/district/ward), hospital type (public/private), services (inpatient/outpatient/dental), and blacklist status.

**Sorting**: Uses `HospitalSortBy` enum with options: Default, NameAscending, NameDescending.

**Pagination**: Managed via `PagingCriteria` class and custom `PagedList<T>` helper. Default page size is 5.

## Development Workflow

When modifying hospital-related features:
1. Update the entity in `Data/Hospital.cs` if schema changes are needed
2. Add migration and update database
3. Update `IHospitalRepository` interface and implementation
4. Modify DTOs if the API contract changes
5. Update ViewModels for UI changes
6. Modify controller actions and views
7. Update localization resources in both `Resources/SharedResource.resx` and `Resources/Views/Hospital/*.resx`
8. Consider HTMX partial view responses for dynamic updates

When adding new services:
1. Create interface in `Services/` directory
2. Implement the service
3. Register in `Program.cs` (use appropriate lifetime: Singleton for MBTileProvider, Scoped for repositories/most services, HttpClient for API services)
