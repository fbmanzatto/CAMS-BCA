using CAMS_BCA.Application.Auctions.Commands.CreateAuction;
using CAMS_BCA.Application.Auctions.Commands.EndAuction;
using CAMS_BCA.Application.Auctions.Commands.StartAuction;
using CAMS_BCA.Application.Auctions.Common;
using CAMS_BCA.Application.Auctions.Queries.GetAuction;
using CAMS_BCA.Contracts.Auctions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CAMS_BCA.Api.Controllers
{
    [Route("auctions")]
    public class AuctionsController(IMediator _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuction(CreateAuctionRequest request)
        {
            var command = new CreateAuctionCommand
            {
                Description = request.Description,
                VehicleId = request.VehicleId,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                auction => CreatedAtAction(
                    actionName: nameof(GetAuction),
                    routeValues: new { auctionId = result.Value.Id },
                    value: ToDto(auction)),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuction(Guid auctionId)
        {
            var query = new GetAuctionQuery(auctionId);

            var result = await _mediator.Send(query);

            return result.Match(
                auction => Ok(ToDto(auction)),
                Problem);
        }

        [HttpPut]
        [Route("start")]
        public async Task<IActionResult> StartAuction(StartAuctionRequest request)
        {
            var command = new StartAuctionCommand
            {
                AuctionId = request.AuctionId,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                auction => CreatedAtAction(
                    actionName: nameof(GetAuction),
                    routeValues: new { auctionId = result.Value.Id },
                    value: ToDto(auction)),
                Problem);
        }

        [HttpPut]
        [Route("end")]
        public async Task<IActionResult> EndAuction(EndAuctionRequest request)
        {
            var command = new EndAuctionCommand
            {
                AuctionId = request.AuctionId,
            };

            var result = await _mediator.Send(command);

            return result.Match(
                auction => CreatedAtAction(
                    actionName: nameof(GetAuction),
                    routeValues: new { auctionId = result.Value.Id },
                    value: ToDto(auction)),
                Problem);
        }

        private static AuctionResponse ToDto(AuctionResult auctionResult) =>
            new(
                auctionResult.Id,
                auctionResult.Description,
                auctionResult.StartDate,
                auctionResult.Active,
                auctionResult.EndDate,
                auctionResult.VehicleId);

        private static List<AuctionResponse> ToDto(List<AuctionResult> auctionResult)
        {
            List<AuctionResponse> auctionResponseDto = auctionResult.Select(v => ToDto(v)).ToList();
            return auctionResponseDto;
        }
    }
}