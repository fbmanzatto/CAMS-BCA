using CAMS_BCA.Application.UnitTests.Common;
using CAMS_BCA.Application.Vehicles.Queries.SearchVehicle;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Vehicles.Commands.SearchVehicle
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class SearchVehicleTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task SearchVehicle_WhenVehicleDoesNotExists_ShouldReturnEmpty()
        {
            // Arrange
            var query = new SearchVehiclesQuery(null, null, null, null);

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(0);
        }

        [Fact]
        public async Task SearchVehicle_WhenVehicleExists_ShouldReturnVehicles()
        {
            // Arrange
            var commandCreate = Constructors.CreateHatchbackVehicleCommand();
            await _mediator.Send(commandCreate);

            var query = new SearchVehiclesQuery(null, null, null, null);

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(1);
        }

        [Fact]
        public async Task SearchVehicleByModel_WhenVehicleExists_ShouldReturnVehicles()
        {
            // Arrange
            var commandCreate = Constructors.CreateHatchbackVehicleCommand();
            await _mediator.Send(commandCreate);

            var query = new SearchVehiclesQuery("Punto Evo", null, null, null);

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(1);
        }

        [Fact]
        public async Task SearchVehicleByType_WhenVehicleExists_ShouldReturnVehicles()
        {
            // Arrange
            var commandCreate = Constructors.CreateHatchbackVehicleCommand();
            await _mediator.Send(commandCreate);

            var query = new SearchVehiclesQuery(null, null, null, VehicleType.Hatchback);

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(1);
        }
    }
}

