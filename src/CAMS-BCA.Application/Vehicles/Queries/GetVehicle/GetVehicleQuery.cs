using CAMS_BCA.Application.Vehicles.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public record GetVehicleQuery(Guid VehicleId)
        : IRequest<ErrorOr<VehicleResult>>;
}