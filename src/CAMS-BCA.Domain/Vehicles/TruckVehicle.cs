namespace CAMS_BCA.Domain.Vehicles
{
    public class TruckVehicle : Vehicle
    {
        public decimal LoadCapacity { get; set; }

        public TruckVehicle()
        {
            Type = VehicleType.Truck;
        }
    }
}