using Vehicles.Common;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public record CreateHatchbackVehicleCommand : CreateVehicleCommand
    {
        public required int NumberOfDoors { get; set; }
    }
}

