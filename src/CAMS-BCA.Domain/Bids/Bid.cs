using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Domain.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

namespace CAMS_BCA.Domain.Bids
{
    public class Bid : Entity
    {
        public required Auction Auction { get; set; }
        public required Vehicle Vehicle { get; set; }
        public required DateTime Date { get; set; }
        public bool Winner { get; set; } = false;
        public decimal Value { get; private set; }

        public ErrorOr<Success> SetValue(decimal value)
        {
            if (value < Vehicle.StartingBid)
            {
                return Error.Conflict(description: "Bid value is lower than Starting Bid");
            }

            Value = value;
            return Result.Success;
        }
    }
}
