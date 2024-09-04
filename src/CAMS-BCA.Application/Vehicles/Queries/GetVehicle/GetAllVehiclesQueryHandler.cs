using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.GetVehicle
{
    public class GetAllVehiclesQueryHandler(IVehiclesRepository _vehiclesRepository)
        : IRequestHandler<GetAllVehiclesQuery, ErrorOr<List<VehicleResult>>>
    {
        public async Task<ErrorOr<List<VehicleResult>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.GetAllAsync(cancellationToken) is List<Vehicle> vehicles
                ? vehicles.Select(v => VehicleResult.FromDomain(v)).ToList()
                : Error.NotFound(description: "Vehicle not found.");
        }
    }
}
