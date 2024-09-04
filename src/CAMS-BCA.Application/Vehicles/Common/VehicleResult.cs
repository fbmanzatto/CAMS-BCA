using CAMS_BCA.Domain.Vehicles;

namespace CAMS_BCA.Application.Vehicles.Common
{
    public record VehicleResult(
        Guid Id,
        string UniqueIdentifier,
        string Model,
        string Manufacturer,
        int Year,
        decimal StartingBid,
        VehicleType Type)
    {
        public static VehicleResult FromDomain(Vehicle vehicle)
        {
            return new VehicleResult(vehicle.Id, vehicle.UniqueIdentifier, vehicle.Model, vehicle.Manufacturer, vehicle.Year, vehicle.StartingBid, vehicle.Type);
        }
    }
}