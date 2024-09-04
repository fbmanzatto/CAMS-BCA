using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Auctions;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Auctions.Commands.CreateAuction
{
    public class CreateAuctionCommandHandler(
        IAuctionsRepository _auctionsRepository, IVehiclesRepository _vehiclesRepository) : IRequestHandler<CreateAuctionCommand, ErrorOr<AuctionResult>>
    {
        public async Task<ErrorOr<AuctionResult>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehiclesRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null)
            {
                return Error.NotFound(description: "Vehicle has not been registered");
            }

            if (!vehicle.Available)
            {
                return Error.Conflict(description: "Vehicle is not Available");
            }

            var auction = new Auction
            {
                Description = request.Description,
                Id = Guid.NewGuid(),
                Vehicle = vehicle,
            };

            await _auctionsRepository.AddAsync(auction, cancellationToken);

            return AuctionResult.FromAuction(auction);
        }
    }
}
