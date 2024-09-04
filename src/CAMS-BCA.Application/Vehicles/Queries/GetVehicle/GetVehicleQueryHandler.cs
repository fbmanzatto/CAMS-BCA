using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler(IVehiclesRepository _vehiclesRepository)
        : IRequestHandler<GetVehicleQuery, ErrorOr<VehicleResult>>
    {
        public async Task<ErrorOr<VehicleResult>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.GetByIdAsync(request.VehicleId, cancellationToken) is Vehicle vehicle
                ? VehicleResult.FromDomain(vehicle)
                : Error.NotFound(description: "Vehicle not found.");
        }
    }
}
