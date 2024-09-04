using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.Bids.Commands.CreateBid;
using CAMS_BCA.Application.UnitTests.Common;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Bids.Commands.CreateBid
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class CreateBidTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task CreateBid_WhenAuctionIsStarted_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand { Description = "Auction Description", VehicleId = resultVehicle.Value.Id };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandStartAuction = new StartAuctionCommand { AuctionId = resultAuction.Value.Id };
            await _mediator.Send(commandStartAuction);

            var commandBid = new CreateBidCommand { AuctionId = resultAuction.Value.Id, VehicleId = resultVehicle.Value.Id, Value = 6000 };

            // Act
            var resultBid = await _mediator.Send(commandBid);

            // Assert
            resultBid.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task CreateBid_WhenAuctionIsNotStarted_ShouldReturnError()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand { Description = "Auction Description", VehicleId = resultVehicle.Value.Id };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandBid = new CreateBidCommand { AuctionId = resultAuction.Value.Id, VehicleId = resultVehicle.Value.Id, Value = 6000 };

            // Act
            var resultBid = await _mediator.Send(commandBid);

            // Assert
            resultBid.IsError.Should().BeTrue();
        }
    }
}

