using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public class GetVehicleByUniqueIdentifierQueryHandler(IVehiclesRepository _vehiclesRepository)
        : IRequestHandler<GetVehicleByUniqueIdentifierQuery, ErrorOr<VehicleResult>>
    {
        public async Task<ErrorOr<VehicleResult>> Handle(GetVehicleByUniqueIdentifierQuery request, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.GetByUniqueIdentifierAsync(request.UniqueIdentifier, cancellationToken) is Vehicle vehicle
                ? VehicleResult.FromDomain(vehicle)
                : Error.NotFound(description: "Vehicle not found.");
        }
    }
}
