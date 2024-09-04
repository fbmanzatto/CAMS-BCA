using CAMS_BCA.Application.Vehicles.Commands.CreateVehicle;
using CAMS_BCA.Application.Vehicles.Common;
using CAMS_BCA.Application.Vehicles.Queries.GetVehicle;
using CAMS_BCA.Application.Vehicles.Queries.SearchVehicle;
using CAMS_BCA.Contracts.Vehicles;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using DomainVehicleType = CAMS_BCA.Domain.Vehicles.VehicleType;
using VehicleType = CAMS_BCA.Contracts.Vehicles.VehicleType;

namespace CAMS_BCA.Api.Controllers
{
    [Route("vehicles")]
    public class VehiclesController(IMediator _mediator) : ApiController
    {
        [HttpPost]
        [Route("hatchback")]
        public async Task<IActionResult> CreateHatchbackVehicle(CreateHatchbackVehicleRequest request)
        {
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = request.UniqueIdentifier,
                Model = request.Model,
                Manufacturer = request.Manufacturer,
                Year = request.Year,
                StartingBid = request.StartingBid,
                Type = DomainVehicleType.Hatchback,
                NumberOfDoors = request.NumberOfDoors,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                vehicle => CreatedAtAction(
                    actionName: nameof(GetVehicle),
                    routeValues: new { vehicleId = result.Value.Id },
                    value: ToDto(vehicle)),
                Problem);
        }

        [HttpPost]
        [Route("sedan")]
        public async Task<IActionResult> CreateSedanVehicle(CreateSedanVehicleRequest request)
        {
            var command = new CreateSedanVehicleCommand
            {
                UniqueIdentifier = request.UniqueIdentifier,
                Model = request.Model,
                Manufacturer = request.Manufacturer,
                Year = request.Year,
                StartingBid = request.StartingBid,
                Type = DomainVehicleType.Sedan,
                NumberOfDoors = request.NumberOfDoors,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                vehicle => CreatedAtAction(
                    actionName: nameof(GetVehicle),
                    routeValues: new { vehicleId = result.Value.Id },
                    value: ToDto(vehicle)),
                Problem);
        }

        [HttpPost]
        [Route("suv")]
        public async Task<IActionResult> CreateSUVVehicle(CreateSUVVehicleRequest request)
        {
            var command = new CreateSUVVehicleCommand
            {
                UniqueIdentifier = request.UniqueIdentifier,
                Model = request.Model,
                Manufacturer = request.Manufacturer,
                Year = request.Year,
                StartingBid = request.StartingBid,
                Type = DomainVehicleType.SUV,
                NumberOfSeats = request.NumberOfSeats,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                vehicle => CreatedAtAction(
                    actionName: nameof(GetVehicle),
                    routeValues: new { vehicleId = result.Value.Id },
                    value: ToDto(vehicle)),
                Problem);
        }

        [HttpPost]
        [Route("truck")]
        public async Task<IActionResult> CreateTruckVehicle(CreateTruckVehicleRequest request)
        {
            var command = new CreateTruckVehicleCommand
            {
                UniqueIdentifier = request.UniqueIdentifier,
                Model = request.Model,
                Manufacturer = request.Manufacturer,
                Year = request.Year,
                StartingBid = request.StartingBid,
                Type = DomainVehicleType.Truck,
                LoadCapacity = request.LoadCapacity,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                vehicle => CreatedAtAction(
                    actionName: nameof(GetVehicle),
                    routeValues: new { vehicleId = result.Value.Id },
                    value: ToDto(vehicle)),
                Problem);
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public async Task<IActionResult> GetVehicle(Guid vehicleId)
        {
            var query = new GetVehicleQuery(vehicleId);

            var result = await _mediator.Send(query);

            return result.Match(
                vehicle => Ok(ToDto(vehicle)),
                Problem);
        }

        [HttpGet]
        [Route("uniqueidentifier")]
        public async Task<IActionResult> GetVehicleByUniqueIdentifier([FromQuery] string uniqueIdentifier)
        {
            var query = new GetVehicleByUniqueIdentifierQuery(uniqueIdentifier);

            var result = await _mediator.Send(query);

            return result.Match(
                vehicle => Ok(ToDto(vehicle)),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var query = new GetAllVehiclesQuery();

            var result = await _mediator.Send(query);

            return result.Match(
                vehicle => Ok(ToDto(vehicle)),
                Problem);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchVehicles(SearchVehicleRequest request)
        {
            DomainVehicleType? vehicleType = null;
            if (request.Type is not null)
            {
                if (!DomainVehicleType.TryFromName(request.Type.ToString(), out vehicleType))
                {
                    return Problem(
                        statusCode: StatusCodes.Status400BadRequest,
                        detail: "Invalid plan type");
                }
            }

            var query = new SearchVehiclesQuery(request.Model, request.Manufacturer, request.Year, vehicleType);

            var result = await _mediator.Send(query);

            return result.Match(
                vehicle => Ok(ToDto(vehicle)),
                Problem);
        }

        private static VehicleType ToDto(DomainVehicleType vehicleType) =>
            vehicleType.Name switch
            {
                nameof(DomainVehicleType.Hatchback) => VehicleType.Hatchback,
                nameof(DomainVehicleType.Sedan) => VehicleType.Sedan,
                nameof(DomainVehicleType.SUV) => VehicleType.SUV,
                nameof(DomainVehicleType.Truck) => VehicleType.Truck,

                _ => throw new InvalidOperationException(),
            };
        private static VehicleResponse ToDto(VehicleResult vehicleResult) =>
            new(
                vehicleResult.Id,
                vehicleResult.UniqueIdentifier,
                vehicleResult.Model,
                vehicleResult.Manufacturer,
                vehicleResult.Year,
                vehicleResult.StartingBid,
                ToDto(vehicleResult.Type));

        private static List<VehicleResponse> ToDto(List<VehicleResult> vehiclesResult)
        {
            List<VehicleResponse> vehicleResponseDto = vehiclesResult.Select(v => ToDto(v)).ToList();
            return vehicleResponseDto;
        }
    }
}