using CAMS_BCA.Application.Bids.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Bids;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Queries.GetBid
{
    public class GetBidQueryHandler(IBidsRepository _bidsRepository)
        : IRequestHandler<GetBidQuery, ErrorOr<BidResult>>
    {
        public async Task<ErrorOr<BidResult>> Handle(GetBidQuery request, CancellationToken cancellationToken)
        {
            return await _bidsRepository.GetByIdAsync(request.BidId, cancellationToken) is Bid bid
                ? BidResult.FromBid(bid)
                : Error.NotFound(description: "Bid not found.");
        }
    }
}
