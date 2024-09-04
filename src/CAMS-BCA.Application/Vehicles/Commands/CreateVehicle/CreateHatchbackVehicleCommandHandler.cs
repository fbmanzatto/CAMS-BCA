using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;
using MediatR;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateHatchbackVehicleCommandHandler(
        IVehiclesRepository _vehiclesRepository) : IRequestHandler<CreateHatchbackVehicleCommand, ErrorOr<VehicleResult>>
    {
        public async Task<ErrorOr<VehicleResult>> Handle(CreateHatchbackVehicleCommand request, CancellationToken cancellationToken)
        {
            if (await _vehiclesRepository.GetByUniqueIdentifierAsync(request.UniqueIdentifier, cancellationToken) is not null)
            {
                return Error.Conflict(description: "Vehicle already has been registered");
            }

            var vehicle = new HatchbackVehicle();
            vehicle.Id = Guid.NewGuid();
            vehicle.UniqueIdentifier = request.UniqueIdentifier;
            vehicle.Model = request.Model;
            vehicle.Manufacturer = request.Manufacturer;
            vehicle.Year = request.Year;
            vehicle.StartingBid = request.StartingBid;
            vehicle.Available = true;
            vehicle.NumberOfDoors = request.NumberOfDoors;

            await _vehiclesRepository.AddAsync(vehicle, cancellationToken);

            return VehicleResult.FromDomain(vehicle);
        }
    }
}
