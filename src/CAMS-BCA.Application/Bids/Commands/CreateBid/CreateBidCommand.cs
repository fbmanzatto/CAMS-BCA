using CAMS_BCA.Application.Bids.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Commands.CreateBid
{
    public record CreateBidCommand : IRequest<ErrorOr<BidResult>>
    {
        public required Guid AuctionId { get; set; }
        public required Guid VehicleId { get; set; }
        public required decimal Value { get; set; }
    }
}

