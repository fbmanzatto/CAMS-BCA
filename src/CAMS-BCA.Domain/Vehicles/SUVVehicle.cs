namespace CAMS_BCA.Domain.Vehicles
{
    public class SUVVehicle : Vehicle
    {
        public int NumberOfSeats { get; set; }

        public SUVVehicle()
        {
            Type = VehicleType.SUV;
        }
    }
}