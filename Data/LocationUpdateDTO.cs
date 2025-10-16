namespace InsuranceWebApp.Data
{
    public sealed record LocationUpdateDTO(GeoPoint point,int? cityId,int? districtId,int? wardId);

}
