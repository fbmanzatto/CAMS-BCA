namespace CAMS_BCA.Domain.Vehicles
{
    public class SedanVehicle : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public SedanVehicle()
        {
            Type = VehicleType.Sedan;
        }
    }
}