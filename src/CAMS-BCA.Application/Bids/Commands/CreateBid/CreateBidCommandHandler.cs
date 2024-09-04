using CAMS_BCA.Application.Bids.Common;
using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Bids;

using ErrorOr;

using MediatR;

namespace CAMS_BCA.Application.Bids.Commands.CreateBid
{
    public class CreateBidCommandHandler(
        IBidsRepository _bidsRepository, IAuctionsRepository _auctionsRepository, IVehiclesRepository _vehiclesRepository) : IRequestHandler<CreateBidCommand, ErrorOr<BidResult>>
    {
        public async Task<ErrorOr<BidResult>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            var auction = await _auctionsRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if (auction is null)
            {
                return Error.NotFound(description: "Auction has not been registered");
            }

            var vehicle = await _vehiclesRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null)
            {
                return Error.NotFound(description: "Vehicle has not been registered");
            }

            var bid = new Bid
            {
                Id = Guid.NewGuid(),
                Auction = auction,
                Date = DateTime.Now,
                Vehicle = vehicle,
            };
            var bidSetValueResult = bid.SetValue(request.Value);

            if (bidSetValueResult.IsError)
            {
                return bidSetValueResult.Errors;
            }

            auction.Bids = await _bidsRepository.GetAllAsync(request.AuctionId, cancellationToken);

            var auctionAddBidResult = auction.AddBid(bid);

            if (auctionAddBidResult.IsError)
            {
                return auctionAddBidResult.Errors;
            }

            await _bidsRepository.AddAsync(bid, cancellationToken);

            return BidResult.FromBid(bid);
        }
    }
}
