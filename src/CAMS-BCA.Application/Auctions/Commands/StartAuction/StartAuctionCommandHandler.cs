using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Common.Interfaces;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.StartAuction
{
    public class StartAuctionCommandHandler(
        IAuctionsRepository _auctionsRepository, IVehiclesRepository _vehiclesRepository) : IRequestHandler<StartAuctionCommand, ErrorOr<AuctionResult>>
    {
        public async Task<ErrorOr<AuctionResult>> Handle(StartAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _auctionsRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if (auction is null)
            {
                return Error.NotFound(description: "Auction has not been registered");
            }

            var vehicleAuctions = await _auctionsRepository.GetAllByVehicleIdAsync(auction.Vehicle.Id, cancellationToken);
            if (vehicleAuctions.Any(a => a.Active))
            {
                return Error.Conflict(description: "Vehicle already in an active Auction");
            }

            var vehicle = await _vehiclesRepository.GetByIdAsync(auction.Vehicle.Id, cancellationToken);
            if (vehicle is not null && !vehicle.Available)
            {
                return Error.Conflict(description: "Vehicle is not available");
            }

            var auctionStartResult = auction.Start();

            if (auctionStartResult.IsError)
            {
                return auctionStartResult.Errors;
            }

            await _auctionsRepository.UpdateAsync(auction, cancellationToken);

            return AuctionResult.FromAuction(auction);
        }
    }
}
