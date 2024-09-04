using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.SearchVehicle
{
    public record SearchVehiclesQuery(
        string? Model,
        string? Manufacturer,
        int? Year,
        VehicleType? Type)
        : IRequest<ErrorOr<List<VehicleResult>>>;
}