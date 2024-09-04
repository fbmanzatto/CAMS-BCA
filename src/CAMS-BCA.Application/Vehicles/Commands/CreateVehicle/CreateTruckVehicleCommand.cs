using Vehicles.Common;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public record CreateTruckVehicleCommand : CreateVehicleCommand
    {
        public required decimal LoadCapacity { get; set; }
    }
}

