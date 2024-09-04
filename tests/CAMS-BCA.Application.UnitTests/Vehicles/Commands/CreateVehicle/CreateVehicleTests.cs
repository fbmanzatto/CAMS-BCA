using CAMS_BCA.Application.UnitTests.Common;
using CAMS_BCA.Application.Vehicles.Commands.CreateVehicle;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Vehicles.Commands.CreateVehicle
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class CreateVehicleTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task CreateVehicle_WhenVehicleDoesNotExists_ShouldReturnSuccess()
        {
            // Arrange
            var command = Constructors.CreateHatchbackVehicleCommand();

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task CreateVehicle_WhenUniqueIdentifierIsShort_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "A",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 2012,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenModelIsShort_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "A",
                Manufacturer = "Fiat",
                Year = 2012,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenManufacturerIsShort_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "Punto Evo",
                Manufacturer = " ",
                Year = 2012,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenYearIsTooOld_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 1900,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenStartingBidLessThanOne_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 2010,
                StartingBid = 0,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 4,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenNumberOfDoorsLessThanOne_ShouldReturnError()
        {
            // Arrange
            var command = new CreateHatchbackVehicleCommand
            {
                UniqueIdentifier = "AA-22-XX",
                Model = "Punto Evo",
                Manufacturer = "Fiat",
                Year = 2010,
                StartingBid = 4000,
                Type = VehicleType.Hatchback,
                NumberOfDoors = 0,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateVehicle_WhenAlreadyExists_ShouldReturnError()
        {
            // Arrange
            var command1 = Constructors.CreateHatchbackVehicleCommand();

            // Act
            var result1 = await _mediator.Send(command1);

            // Arrange
            var command2 = Constructors.CreateHatchbackVehicleCommand();

            // Act
            var result2 = await _mediator.Send(command2);

            // Assert
            result1.IsError.Should().BeFalse();
            result2.IsError.Should().BeTrue();
        }
    }
}

