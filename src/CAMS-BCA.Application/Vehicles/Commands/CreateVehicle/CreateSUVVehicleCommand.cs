using Vehicles.Common;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public record CreateSUVVehicleCommand : CreateVehicleCommand
    {
        public required int NumberOfSeats { get; set; }
    }
}

