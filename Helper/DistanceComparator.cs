using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Helper
{
    public class DistanceComparator : IComparer<HospitalDTO>
    {
        private readonly GeoPoint currentPosition;

        public DistanceComparator(GeoPoint currentPosition)
        {
            this.currentPosition = currentPosition;
        }
        public int Compare(HospitalDTO? x, HospitalDTO? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            var longitudeX = x.Longitude;
            var latitudeX = y.Latitude;
            var longitudeY = y.Longitude;
            var latitudeY = y.Latitude;

            var d1 = GeoCodingExtension.HaversineDistance(currentPosition.Latitude, currentPosition.Longitude, latitudeX, longitudeX);
            var d2 = GeoCodingExtension.HaversineDistance(currentPosition.Latitude, currentPosition.Longitude, latitudeY, longitudeY);

            return d1.CompareTo(d2);
        }
    }
}
