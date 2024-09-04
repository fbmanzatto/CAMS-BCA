using CAMS_BCA.Application.Bids.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Queries.GetBid
{
    public record GetAllBidsQuery()
        : IRequest<ErrorOr<List<BidResult>>>;
}