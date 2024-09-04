using CAMS_BCA.Application.Vehicles.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public record GetAllVehiclesQuery()
        : IRequest<ErrorOr<List<VehicleResult>>>;
}