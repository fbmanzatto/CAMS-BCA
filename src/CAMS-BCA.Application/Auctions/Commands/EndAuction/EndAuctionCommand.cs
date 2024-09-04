using CAMS_BCA.Application.Auctions.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.EndAuction
{
    public record EndAuctionCommand : IRequest<ErrorOr<AuctionResult>>
    {
        public required Guid AuctionId { get; set; }
    }
}

