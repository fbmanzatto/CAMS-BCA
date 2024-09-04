using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.UnitTests.Common;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Auctions.Commands.StartAuction
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class StartAuctionTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task StartAuction_WhenVehicleExists_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = resultVehicle.Value.Id,
            };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandStartAuction = new StartAuctionCommand { AuctionId = resultAuction.Value.Id };

            // Act
            var resultStartAuction = await _mediator.Send(commandStartAuction);

            // Assert
            resultStartAuction.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task StartAuction_WhenVehicleAlreadyInActiveAuction_ShouldReturnError()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = resultVehicle.Value.Id,
            };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandStartAuction = new StartAuctionCommand { AuctionId = resultAuction.Value.Id };
            var resultStartAuction = await _mediator.Send(commandStartAuction);

            var commandAuction2 = new CreateAuctionCommand
            {
                Description = "Auction Description 2",
                VehicleId = resultVehicle.Value.Id,
            };

            var resultAuction2 = await _mediator.Send(commandAuction2);

            var commandStartAuction2 = new StartAuctionCommand { AuctionId = resultAuction2.Value.Id };

            // Act
            var resultStartAuction2 = await _mediator.Send(commandStartAuction2);

            // Assert
            resultStartAuction2.IsError.Should().BeTrue();
        }
    }
}

