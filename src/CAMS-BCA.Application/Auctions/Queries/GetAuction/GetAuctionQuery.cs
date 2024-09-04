using CAMS_BCA.Application.Auctions.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Queries.GetAuction
{
    public record GetAuctionQuery(Guid AuctionId)
        : IRequest<ErrorOr<AuctionResult>>;
}