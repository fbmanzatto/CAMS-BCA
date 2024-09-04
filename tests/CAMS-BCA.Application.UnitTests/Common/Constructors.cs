using CAMS_BCA.Application.Vehicles.Commands.CreateVehicle;
using CAMS_BCA.Domain.Vehicles;

namespace CAMS_BCA.Application.UnitTests
{
    public static class Constructors
    {
        public static CreateHatchbackVehicleCommand CreateHatchbackVehicleCommand()
        {
            return new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 2010,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };
        }
    }
}
