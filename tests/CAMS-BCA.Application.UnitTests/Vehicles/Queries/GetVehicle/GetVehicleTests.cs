using CAMS_BCA.Application.UnitTests.Common;
using CAMS_BCA.Application.Vehicles.Commands.CreateVehicle;
using CAMS_BCA.Application.Vehicles.Queries.GetVehicle;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Vehicles.Commands.GetVehicle
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class GetVehicleTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task GetVehicle_WhenVehicleDoesNotExists_ShouldReturnError()
        {
            // Arrange
            var query = new GetVehicleQuery(Guid.NewGuid());

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public async Task GetVehicle_WhenVehicleExists_ShouldReturnSuccess()
        {
            // Arrange
            var command = Constructors.CreateHatchbackVehicleCommand();

            var result = await _mediator.Send(command);

            var query = new GetVehicleQuery(result.Value.Id);

            // Act
            var resultGet = await _mediator.Send(query);

            // Assert
            resultGet.IsError.Should().BeFalse();
            resultGet.Value.UniqueIdentifier.Should().Be(command.UniqueIdentifier);
        }

        [Fact]
        public async Task GetVehicleByUniqueIdentifier_WhenVehicleExists_ShouldReturnSuccess()
        {
            // Arrange
            var command = Constructors.CreateHatchbackVehicleCommand();

            await _mediator.Send(command);

            var query = new GetVehicleByUniqueIdentifierQuery(command.UniqueIdentifier);

            // Act
            var resultGet = await _mediator.Send(query);

            // Assert
            resultGet.IsError.Should().BeFalse();
            resultGet.Value.UniqueIdentifier.Should().Be(command.UniqueIdentifier);
        }

        [Fact]
        public async Task GetAllVehicle_WhenVehicleExists_ShouldReturnSuccess()
        {
            // Arrange
            var command1 = Constructors.CreateHatchbackVehicleCommand();

            await _mediator.Send(command1);

            var command2 = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-ZZ",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 2013,
                StartingBid = 5000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };
            await _mediator.Send(command2);

            var query = new GetAllVehiclesQuery();

            // Act
            var resultGet = await _mediator.Send(query);

            // Assert
            resultGet.IsError.Should().BeFalse();
            resultGet.Value.Count().Should().Be(2);
        }
    }
}

