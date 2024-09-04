using CAMS_BCA.Application.Bids.Commands.CreateBid;
using CAMS_BCA.Application.Bids.Common;
using CAMS_BCA.Application.Bids.Queries.GetBid;
using CAMS_BCA.Contracts.Bids;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CAMS_BCA.Api.Controllers
{
    [Route("bids")]
    public class BidsController(IMediator _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBid(CreateBidRequest request)
        {
            var command = new CreateBidCommand
            {
                AuctionId = request.AuctionId,
                VehicleId = request.VehicleId,
                Value = request.Value,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                bid => CreatedAtAction(
                    actionName: nameof(GetBid),
                    routeValues: new { bidId = result.Value.Id },
                    value: ToDto(bid)),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetBid(Guid bidId)
        {
            var query = new GetBidQuery(bidId);

            var result = await _mediator.Send(query);
            return result.Match(
                bid => Ok(ToDto(bid)),
                Problem);
        }

        private static BidResponse ToDto(BidResult bidResult) =>
            new(
                bidResult.Id,
                bidResult.Auction.Id,
                bidResult.Vehicle.Id,
                bidResult.Date,
                bidResult.Value);

        private static List<BidResponse> ToDto(List<BidResult> bidResult)
        {
            List<BidResponse> bidResponseDto = bidResult.Select(v => ToDto(v)).ToList();
            return bidResponseDto;
        }
    }
}