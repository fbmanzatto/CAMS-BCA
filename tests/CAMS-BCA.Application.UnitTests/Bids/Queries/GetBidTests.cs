using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.Bids.Commands.CreateBid;
using CAMS_BCA.Application.Bids.Queries.GetBid;
using CAMS_BCA.Application.UnitTests.Common;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Bids.Queries.GetBid
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class GetBidTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task GetBid_WhenExists_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            var commandAuction = new CreateAuctionCommand { Description = "Auction Description", VehicleId = resultVehicle.Value.Id };
            var resultAuction = await _mediator.Send(commandAuction);

            var commandStartAuction = new StartAuctionCommand { AuctionId = resultAuction.Value.Id };
            await _mediator.Send(commandStartAuction);

            var commandBid = new CreateBidCommand { AuctionId = resultAuction.Value.Id, VehicleId = resultVehicle.Value.Id, Value = 6000 };
            var resultBid = await _mediator.Send(commandBid);

            var query = new GetBidQuery(resultBid.Value.Id);

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(resultBid.Value.Id);
        }

        [Fact]
        public async Task GetBid_WhenDoesNotExists_ShouldReturnError()
        {
            var query = new GetBidQuery(Guid.NewGuid());

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllBid_WhenExists_ShouldReturnSuccess()
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

            // Act
            var query = new GetAllBidsQuery();
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetAllBid_WhenNotExists_ShouldReturnEmpty()
        {
            // Arrange
            var query = new GetAllBidsQuery();

            // Act
            var result = await _mediator.Send(query);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Count.Should().Be(0);
        }
    }
}

