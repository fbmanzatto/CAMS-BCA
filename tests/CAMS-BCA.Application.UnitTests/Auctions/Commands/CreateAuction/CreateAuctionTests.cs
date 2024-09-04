using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.EndAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.Bids.Commands.CreateBid;
using CAMS_BCA.Application.UnitTests.Common;

using ErrorOr;

using FluentAssertions;

using MediatR;

using Xunit;

namespace CAMS_BCA.Application.UnitTests.Auctions.Commands.CreateAuction
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class CreateAuctionTests(WebAppFactory webAppFactory)
    {
        private readonly IMediator _mediator = webAppFactory.CreateMediator();

        [Fact]
        public async Task CreateAuction_WhenDescriptionIsShort_ShouldReturnError()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();

            var resultVehicle = await _mediator.Send(commandVehicle);

            var command = new CreateAuctionCommand
            {
                Description = " ",
                VehicleId = resultVehicle.Value.Id,
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAuction_WhenVehicleDoesNotExists_ShouldReturnNotFound()
        {
            // Arrange
            var command = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = Guid.Parse("A07CA580-B333-4268-8EC7-5C9B6081CC09"),
            };

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public async Task CreateAuction_WhenVehicleExists_ShouldReturnSuccess()
        {
            // Arrange
            var commandVehicle = Constructors.CreateHatchbackVehicleCommand();
            var resultVehicle = await _mediator.Send(commandVehicle);

            // Act
            var command = new CreateAuctionCommand
            {
                Description = "Auction Description",
                VehicleId = resultVehicle.Value.Id,
            };

            var result = await _mediator.Send(command);

            // Assert
            result.IsError.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAuction_WhenVehicleIsNotAvailable_ShouldReturnError()
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
            await _mediator.Send(commandEnd);

            var commandAuction2 = new CreateAuctionCommand { Description = "Auction Description 2", VehicleId = resultVehicle.Value.Id };

            // Act
            var resultAuction2 = await _mediator.Send(commandAuction2);

            // Assert
            resultAuction2.IsError.Should().BeTrue();
        }
    }
}

