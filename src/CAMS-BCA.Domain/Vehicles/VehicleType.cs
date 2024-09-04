using Ardalis.SmartEnum;

namespace CAMS_BCA.Domain.Vehicles
{
    public class VehicleType(string name, int value)
        : SmartEnum<VehicleType>(name, value)
    {
        public static readonly VehicleType Hatchback = new(nameof(Hatchback), 0);
        public static readonly VehicleType Sedan = new(nameof(Sedan), 1);
        public static readonly VehicleType SUV = new(nameof(SUV), 2);
        public static readonly VehicleType Truck = new(nameof(Truck), 3);
    }
}