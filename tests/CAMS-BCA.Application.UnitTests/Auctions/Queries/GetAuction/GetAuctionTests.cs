using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Queries.GetAuction;
using CAMS_BCA.Application.UnitTests.Common;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Auctions.Queries.GetAuction
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class GetAuctionTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task GetAuction_WhenExists_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);
            var command = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = resultVehicle.Value.Id,
            };

            var resultCreate = await _mediator.Send(command);

            // Act
            var query = new GetAuctionQuery(resultCreate.Value.Id);

            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task GetAuction_WhenDoesNotExists_ShouldReturnError()
        {
            var query = new GetAuctionQuery(Guid.NewGuid());

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllAuction_WhenExists_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);
            var command = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = resultVehicle.Value.Id,
            };

            await _mediator.Send(command);

            // Act
            var query = new GetAllAuctionsQuery();
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetAllAuction_WhenNotExists_ShouldReturnEmpty()
        {
            // Arrange
            var query = new GetAllAuctionsQuery();

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(0);
        }
    }
}

