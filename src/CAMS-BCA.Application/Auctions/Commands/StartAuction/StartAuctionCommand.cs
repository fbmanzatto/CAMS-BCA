using CAMS_BCA.Application.Auctions.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.StartAuction
{
    public record StartAuctionCommand : IRequest<ErrorOr<AuctionResult>>
    {
        public required Guid AuctionId { get; set; }
    }
}

