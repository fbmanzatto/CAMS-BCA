using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Domain.Bids;
using CAMS_BCA.Domain.Vehicles;

namespace CAMS_BCA.Application.Bids.Common
{
    public record BidResult(
        Guid Id,
        Auction Auction,
        Vehicle Vehicle,
        DateTime Date,
        decimal Value)
    {
        public static BidResult FromBid(Bid bid)
        {
            return new BidResult(bid.Id, bid.Auction, bid.Vehicle, bid.Date, bid.Value);
        }
    }
}