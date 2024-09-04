using CAMS_BCA.Application.Bids.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Bids;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Queries.GetBid
{
    public class GetAllBidsQueryHandler(IBidsRepository _bidsRepository)
        : IRequestHandler<GetAllBidsQuery, ErrorOr<List<BidResult>>>
    {
        public async Task<ErrorOr<List<BidResult>>> Handle(GetAllBidsQuery request, CancellationToken cancellationToken)
        {
            return await _bidsRepository.GetAllAsync(cancellationToken) is List<Bid> bids
                ? bids.Select(a => BidResult.FromBid(a)).ToList()
                : Error.NotFound(description: "Bids not found.");
        }
    }
}
