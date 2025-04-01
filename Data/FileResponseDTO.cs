namespace InsuranceWebApp.Data
{
    public sealed record FileResponseDTO(
        string HospitalName,
        string HospitalAddress,
        string? PhoneNumber,
        string? BillingTime,
        string? InsuranceAndDirectBilling,
        string? Note,
        string? CityName,
        string? DistrictName,
        string? WardName,
        bool? IsPublicHospital,
        bool? InPatient,
        bool? OutPatient,
        bool? Dental
    );

}
