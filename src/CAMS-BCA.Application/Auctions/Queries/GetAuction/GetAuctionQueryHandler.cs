using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Auctions;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Queries.GetAuction
{
    public class GetAuctionQueryHandler(IAuctionsRepository _auctionsRepository)
        : IRequestHandler<GetAuctionQuery, ErrorOr<AuctionResult>>
    {
        public async Task<ErrorOr<AuctionResult>> Handle(GetAuctionQuery request, CancellationToken cancellationToken)
        {
            return await _auctionsRepository.GetByIdAsync(request.AuctionId, cancellationToken) is Auction auction
                ? AuctionResult.FromAuction(auction)
                : Error.NotFound(description: "Auction not found.");
        }
    }
}
