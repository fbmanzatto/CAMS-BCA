namespace CAMS_BCA.Domain.Vehicles
{
    public class HatchbackVehicle : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public HatchbackVehicle()
        {
            Type = VehicleType.Hatchback;
        }
    }
}