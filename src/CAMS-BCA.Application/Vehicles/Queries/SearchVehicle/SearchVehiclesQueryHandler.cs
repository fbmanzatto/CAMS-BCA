using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Vehicles.Queries.SearchVehicle
{
    public class SearchVehiclesQueryHandler(IVehiclesRepository _vehiclesRepository)
        : IRequestHandler<SearchVehiclesQuery, ErrorOr<List<VehicleResult>>>
    {
        public async Task<ErrorOr<List<VehicleResult>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.SearchAsync(request.Model, request.Manufacturer, request.Year, request.Type, cancellationToken) is List<Vehicle> vehicles
                ? vehicles.Select(v => VehicleResult.FromDomain(v)).ToList()
                : Error.NotFound(description: "Vehicle not found.");
        }
    }
}
