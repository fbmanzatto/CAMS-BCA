using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Common.Interfaces;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.EndAuction
{
    public class EndAuctionCommandHandler(
        IAuctionsRepository _auctionsRepository, IBidsRepository _bidsRepository, IVehiclesRepository _vehicleRepository) : IRequestHandler<EndAuctionCommand, ErrorOr<AuctionResult>>
    {
        public async Task<ErrorOr<AuctionResult>> Handle(EndAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _auctionsRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if (auction is null)
            {
                return Error.NotFound(description: "Auction has not been registered");
            }

            var bids = await _bidsRepository.GetAllAsync(request.AuctionId, cancellationToken);

            auction.Bids = bids;

            var auctionEndResult = auction.End();
            if (auctionEndResult.IsError)
            {
                return auctionEndResult.Errors;
            }

            var auctionGetWinnerBidResult = auction.GetWinnerBid();
            if (auctionGetWinnerBidResult.IsError)
            {
                return auctionGetWinnerBidResult.Errors;
            }

            // TODO: UnitOfWork
            if (auction.HasWinner())
            {
                await _bidsRepository.UpdateAsync(auctionGetWinnerBidResult.Value, cancellationToken);
            }

            await _vehicleRepository.UpdateAsync(auction.Vehicle, cancellationToken);
            await _auctionsRepository.UpdateAsync(auction, cancellationToken);

            return AuctionResult.FromAuction(auction);
        }
    }
}
