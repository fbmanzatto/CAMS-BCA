using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.EndAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.Bids.Commands.CreateBid;
using CAMS_BCA.Application.UnitTests.Common;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Auctions.Commands.EndAuction
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class EndAuctionTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task EndAuction_WhenIsStarted_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand { Description = "Auction Description", VehicleId = resultVehicle.Value.Id };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandStartAuction = new StartAuctionCommand { AuctionId = resultAuction.Value.Id };
            await _mediator.Send(commandStartAuction);

            var commandBid = new CreateBidCommand { AuctionId = resultAuction.Value.Id, VehicleId = resultVehicle.Value.Id, Value = 6000 };
            await _mediator.Send(commandBid);

            var commandEnd = new EndAuctionCommand { AuctionId = resultAuction.Value.Id };

            // Act
            var resultEnd = await _mediator.Send(commandEnd);

            // Assert
            resultEnd.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task EndAuction_WhenIsNotStarted_ShouldReturnError()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand { Description = "Auction Description", VehicleId = resultVehicle.Value.Id };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandEnd = new EndAuctionCommand { AuctionId = resultAuction.Value.Id };

            // Act
            var resultEnd = await _mediator.Send(commandEnd);

            // Assert
            resultEnd.IsError.Should().BeTrue();
        }
    }
}

