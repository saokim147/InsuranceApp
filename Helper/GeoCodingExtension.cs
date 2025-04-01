using InsuranceWebApp.Data;
using InsuranceWebApp.Models;


namespace InsuranceWebApp.Helper
{
    public static class GeoCodingExtension
    {
        public static List<HospitalDTO> FindNearByLocation(this GeoPoint currentPosition, List<HospitalDTO> locations, int range)
        {
            locations.Sort(new DistanceComparator(currentPosition));// tu gan nhat den xa nhat
            if (locations.Count <= range)
            {
                return locations;
            }
            return locations.Take(range).ToList();
        }

        public static decimal HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const decimal EarthRadius = 6371000;

            double lat1Rad = ToRadians(lat1);
            double lon1Rad = ToRadians(lon1);
            double lat2Rad = ToRadians(lat2);
            double lon2Rad = ToRadians(lon2);
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;
            double a = (Math.Sin((double)deltaLat / 2) * Math.Sin((double)deltaLat / 2) +
                       Math.Cos((double)lat1Rad) * Math.Cos((double)lat2Rad) *
                       Math.Sin((double)deltaLon / 2) * Math.Sin((double)deltaLon / 2));

            var c = (decimal)(2 * Math.Atan2(Math.Sqrt((double)a), Math.Sqrt((double)(1 - a))));

            // Distance in meters
            return EarthRadius * c;
        }
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }
    }
}
