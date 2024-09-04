using CAMS_BCA.Application.Auctions.Common;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.CreateAuction
{
    public record CreateAuctionCommand : IRequest<ErrorOr<AuctionResult>>
    {
        public required string Description { get; set; }
        public required Guid VehicleId { get; set; }
    }
}

