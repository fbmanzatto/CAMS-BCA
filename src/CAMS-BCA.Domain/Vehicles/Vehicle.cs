using CAMS_BCA.Domain.Common;

namespace CAMS_BCA.Domain.Vehicles
{
    public abstract class Vehicle : Entity
    {
        public string UniqueIdentifier { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public int Year { get; set; }
        public decimal StartingBid { get; set; }
        public VehicleType Type { get; set; } = null!;
        public bool Available { get; set; } = true;

        internal Vehicle()
        {
        }
    }
}