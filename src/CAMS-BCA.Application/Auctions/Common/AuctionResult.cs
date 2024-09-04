using CAMS_BCA.Domain.Auctions;

namespace CAMS_BCA.Application.Auctions.Common
{
    public record AuctionResult(
        Guid Id,
        string Description,
        DateTime StartDate,
        bool Active,
        DateTime EndDate,
        Guid VehicleId)
    {
        public static AuctionResult FromAuction(Auction auction)
        {
            return new AuctionResult(auction.Id, auction.Description, auction.StartDate, auction.Active, auction.EndDate, auction.Vehicle.Id);
        }
    }
}