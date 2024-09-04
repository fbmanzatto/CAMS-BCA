using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Domain.UnitTests.Common;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

using Xunit;

namespace CAMS_BCA.Domain.UnitTests.Auctions
{
    public class AuctionsTests : TestsBase
    {
        [Fact]
        public void CreateAuction_WhenConstructedSuccessfully_ShouldHaveWinnerBidFalse()
        {
            // Arrange || Act
            var auction = Constructors.CreateAuction(true);

            // Assert
            auction.HasWinner().Should().BeFalse();
        }

        [Fact]
        public void CreateAuction_WhenConstructedSuccessfully_ShouldNotBeActive()
        {
            // Arrange || Act
            var auction = new Auction { Description = "Description", Vehicle = new HatchbackVehicle() };

            // Assert
            auction.Active.Should().BeFalse();
        }

        [Fact]
        public void StartAuction_WhenNotActive_ShouldReturnSuccess()
        {
            // Arrange
            var auction = Constructors.CreateAuction(false);

            // Act
            var auctionStartResponse = auction.Start();

            // Assert
            auction.Active.Should().BeTrue();
            auction.StartDate.Should().BeAfter(DateTime.MinValue);
            auction.EndDate.Should().Be(DateTime.MinValue);
            auctionStartResponse.IsError.Should().BeFalse();
        }

        [Fact]
        public void StartAuction_WhenAlreadyActive_ShouldReturnError()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);

            // Act
            var auctionStartResponse = auction.Start();

            // Assert
            auctionStartResponse.IsError.Should().BeTrue();
        }

        [Fact]
        public void EndAuction_WhenIsActive_ShouldReturnSuccess()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);

            // Act
            var auctionEndResponse = auction.End();

            // Assert
            auctionEndResponse.IsError.Should().BeFalse();
            auction.Active.Should().BeFalse();
            auction.StartDate.Should().BeAfter(DateTime.MinValue);
            auction.EndDate.Should().BeAfter(DateTime.MinValue);
        }

        [Fact]
        public void EndAuction_WhenAlreadyNotActive_ShouldReturnError()
        {
            // Arrange
            var auction = Constructors.CreateAuction(false);

            // Act
            var auctionEndResponse = auction.End();

            // Assert
            auctionEndResponse.IsError.Should().BeTrue();
        }

        [Fact]
        public void AddBidInAuction_WhenIsActive_ShouldReturnSuccess()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);
            var bid = Constructors.CreateBid(auction, 6000);

            // Act
            var auctionAddBidResponse = auction.AddBid(bid);

            // Assert
            auctionAddBidResponse.IsError.Should().BeFalse();
        }

        [Fact]
        public void AddBidInAuction_WhenWhenNotActive_ShouldReturnError()
        {
            // Arrange
            var auction = Constructors.CreateAuction(false);
            var bid = Constructors.CreateBid(auction, 6000);

            // Act
            var auctionAddBidResponse = auction.AddBid(bid);

            // Assert
            auctionAddBidResponse.IsError.Should().BeTrue();
        }

        [Fact]
        public void AddBidInAuction_WhenIsGreaterThanBestBidAndActive_ShouldReturnSuccess()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);
            var bid1 = Constructors.CreateBid(auction, 6000);
            var bid2 = Constructors.CreateBid(auction, 7000);

            // Act
            var auctionAddBid1Response = auction.AddBid(bid1);
            var auctionAddBid2Response = auction.AddBid(bid2);

            // Assert
            auctionAddBid1Response.IsError.Should().BeFalse();
            auctionAddBid2Response.IsError.Should().BeFalse();
        }

        [Fact]
        public void AddBidInAuction_WhenIsGreaterThanBestBidAndActive_ShouldReturnError()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);
            var bid1 = Constructors.CreateBid(auction, 6000);
            var bid2 = Constructors.CreateBid(auction, 5000);

            // Act
            var auctionAddBid1Response = auction.AddBid(bid1);
            var auctionAddBid2Response = auction.AddBid(bid2);

            // Assert
            auctionAddBid1Response.IsError.Should().BeFalse();
            auctionAddBid2Response.IsError.Should().BeTrue();
        }

        [Fact]
        public void BestBidInAuction_WhenHaveMultipleBids_ShouldBeTheGreatestValue()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);
            var bid1 = Constructors.CreateBid(auction, 6000);
            var bid2 = Constructors.CreateBid(auction, 7000);
            auction.AddBid(bid1);
            auction.AddBid(bid2);

            // Act
            var bestBid = auction.GetBestBid();

            // Assert
            bestBid.Should().NotBeNull();
            bestBid?.Value.Should().Be(7000);
        }

        [Fact]
        public void EndAuction_WhenHaveMultipleBids_ShouldHaveaWinnerBid()
        {
            // Arrange
            var auction = Constructors.CreateAuction(true);
            var bid1 = Constructors.CreateBid(auction, 6000);
            var bid2 = Constructors.CreateBid(auction, 7000);
            auction.AddBid(bid1);
            auction.AddBid(bid2);

            // Act
            var auctionEndResponse = auction.End();

            // Assert
            auctionEndResponse.IsError.Should().BeFalse();
            auction.HasWinner().Should().BeTrue();
            auction.GetWinnerBid().Should().NotBeNull();
        }
    }
}