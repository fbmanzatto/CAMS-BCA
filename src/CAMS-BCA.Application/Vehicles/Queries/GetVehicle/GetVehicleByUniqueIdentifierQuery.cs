using CAMS_BCA.Application.Vehicles.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public record GetVehicleByUniqueIdentifierQuery(string UniqueIdentifier)
        : IRequest<ErrorOr<VehicleResult>>;
}