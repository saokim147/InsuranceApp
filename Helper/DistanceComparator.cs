using InsuranceWebApp.Data;
using InsuranceWebApp.Models;

namespace InsuranceWebApp.Helper
{
    public class DistanceComparator : IComparer<HospitalViewModel>
    {
        private readonly GeoPoint currentPosition;

        public DistanceComparator(GeoPoint currentPosition)
        {
            this.currentPosition = currentPosition;
        }
        public int Compare(HospitalViewModel? x, HospitalViewModel? y)
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
            var longitudeX = x.Longitude ?? 0;
            var latitudeX = y.Latitude ?? 0;
            var longitudeY = y.Longitude ?? 0;
            var latitudeY= y.Latitude ?? 0;

            var d1 = GeoCodingExtension.HaversineDistance(currentPosition.Latitude, currentPosition.Longitude,latitudeX,longitudeX);
            var d2 = GeoCodingExtension.HaversineDistance(currentPosition.Latitude, currentPosition.Longitude, latitudeY, longitudeY);

            return d1.CompareTo(d2);
        }
    }
}
