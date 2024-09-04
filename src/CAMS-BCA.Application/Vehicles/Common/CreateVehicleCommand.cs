using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace Vehicles.Common
{
    public abstract record CreateVehicleCommand : IRequest<ErrorOr<VehicleResult>>
    {
        public required string UniqueIdentifier { get; init; }
        public required string Model { get; init; }
        public required string Manufacturer { get; init; }
        public required int Year { get; init; }
        public required decimal StartingBid { get; init; }
        public required VehicleType Type { get; init; }
    }
}

