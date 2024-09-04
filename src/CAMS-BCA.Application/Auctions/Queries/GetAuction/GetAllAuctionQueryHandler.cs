using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Auctions;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Queries.GetAuction
{
    public class GetAllAuctionQueryHandler(IAuctionsRepository _auctionsRepository)
        : IRequestHandler<GetAllAuctionsQuery, ErrorOr<List<AuctionResult>>>
    {
        public async Task<ErrorOr<List<AuctionResult>>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
        {
            return await _auctionsRepository.GetAllAsync(cancellationToken) is List<Auction> auctions
                ? auctions.Select(a => AuctionResult.FromAuction(a)).ToList()
                : Error.NotFound(description: "Auctions not found.");
        }
    }
}
