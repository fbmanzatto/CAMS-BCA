using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;
using MediatR;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateSUVVehicleCommandHandler(
        IVehiclesRepository _vehiclesRepository) : IRequestHandler<CreateSUVVehicleCommand, ErrorOr<VehicleResult>>
    {
        public async Task<ErrorOr<VehicleResult>> Handle(CreateSUVVehicleCommand request, CancellationToken cancellationToken)
        {
            if (await _vehiclesRepository.GetByUniqueIdentifierAsync(request.UniqueIdentifier, cancellationToken) is not null)
            {
                return Error.Conflict(description: "Vehicle already has been registered");
            }

            var vehicle = new SUVVehicle();
            vehicle.Id = Guid.NewGuid();
            vehicle.UniqueIdentifier = request.UniqueIdentifier;
            vehicle.Model = request.Model;
            vehicle.Manufacturer = request.Manufacturer;
            vehicle.Year = request.Year;
            vehicle.StartingBid = request.StartingBid;
            vehicle.Available = true;
            vehicle.NumberOfSeats = request.NumberOfSeats;

            await _vehiclesRepository.AddAsync(vehicle, cancellationToken);

            return VehicleResult.FromDomain(vehicle);
        }
    }
}
