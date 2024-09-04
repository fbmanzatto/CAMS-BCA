using CAMS_BCA.Domain.UnitTests.Common;

using FluentAssertions;

using Xunit;

namespace CAMS_BCA.Domain.UnitTests.Bids
{
    public class BidsTests : TestsBase
    {
        [Fact]
        public void CreateBid_WhenConstructedSuccessfully_ShouldHaveWinnerFalse()
        {
            // Arrange || Act
            var auction = Constructors.CreateAuction(true);
            var bid = Constructors.CreateBid(auction, 2000);

            // Assert
            bid.Winner.Should().BeFalse();
        }

        [Fact]
        public void BidSetValue_WhenIsGreaterThanVehicleStartingBid_ShouldReturnSuccess()
        {
            // Arrange
            var vehicle = Constructors.CreateHatchbackVehicle(2000);
            var auction = Constructors.CreateAuction(vehicle, true);
            var bid = Constructors.CreateBid(auction, vehicle);

            // Act
            var bidSetValueResponse = bid.SetValue(3000);

            // Assert
            bidSetValueResponse.IsError.Should().BeFalse();
        }

        [Fact]
        public void BidSetValue_WhenIsSmallerThanVehicleStartingBid_ShouldReturnError()
        {
            // Arrange
            var vehicle = Constructors.CreateHatchbackVehicle(2000);
            var auction = Constructors.CreateAuction(vehicle, true);
            var bid = Constructors.CreateBid(auction, vehicle);

            // Act
            var bidSetValueResponse = bid.SetValue(1999);

            // Assert
            bidSetValueResponse.IsError.Should().BeTrue();
        }
    }
}