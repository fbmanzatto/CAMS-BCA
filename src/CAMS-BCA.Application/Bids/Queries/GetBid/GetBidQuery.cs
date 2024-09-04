using CAMS_BCA.Application.Bids.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Queries.GetBid
{
    public record GetBidQuery(Guid BidId)
        : IRequest<ErrorOr<BidResult>>;
}