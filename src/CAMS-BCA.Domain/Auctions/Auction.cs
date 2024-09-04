using CAMS_BCA.Domain.Bids;
using CAMS_BCA.Domain.Common;
using CAMS_BCA.Domain.Vehicles;

using ErrorOr;

namespace CAMS_BCA.Domain.Auctions
{
    public class Auction : Entity
    {
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public bool Active { get; set; }
        public DateTime EndDate { get; set; }
        public required Vehicle Vehicle { get; set; } = null!;
        public List<Bid> Bids { private get; set; }
        private Bid WinnerBid { get; set; } = null!;

        public Auction()
        {
            Bids = new List<Bid>();
        }

        public ErrorOr<Success> Start()
        {
            if (Active)
            {
                return Error.Conflict(description: "Auction is already started");
            }
            else
            {
                Active = true;
                StartDate = DateTime.Now;
                EndDate = DateTime.MinValue;
                return Result.Success;
            }
        }

        public ErrorOr<Success> End()
        {
            if (!Active)
            {
                return Error.Conflict(description: "Auction is not started");
            }
            else
            {
                Active = false;
                EndDate = DateTime.Now;
                SetWinnerBestBid();
                Vehicle.Available = WinnerBid is null;
                return Result.Success;
            }
        }

        public ErrorOr<Success> AddBid(Bid bid)
        {
            if (!Active)
            {
                return Error.Conflict(description: "Auction is not started");
            }

            var bestBid = GetBestBid();
            if (bestBid is not null && bid.Value <= bestBid.Value)
            {
                return Error.Conflict(description: "Bid is lower than current best Bid");
            }

            Bids.Add(bid);
            return Result.Success;
        }

        public Bid? GetBestBid()
        {
            if (Bids.Count > 0)
            {
               return Bids.MaxBy(bid => bid.Value);
            }
            else
            {
                return null;
            }
        }

        public ErrorOr<Bid> GetWinnerBid()
        {
            if (Active)
            {
                return Error.Conflict(description: "Auction is not closed");
            }

            return WinnerBid;
        }

        public bool HasWinner()
        {
            return WinnerBid != null;
        }

        private void SetWinnerBestBid()
        {
            var winnerBid = GetBestBid();
            if (winnerBid is not null)
            {
                winnerBid.Winner = true;
                WinnerBid = winnerBid;
            }
        }
    }
}
