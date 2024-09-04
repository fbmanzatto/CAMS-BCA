using FluentValidation;

namespace CAMS_BCA.Application.Auctions.Commands.EndAuction
{
    public class EndAuctionCommandValidator : AbstractValidator<EndAuctionCommand>
    {
        public EndAuctionCommandValidator()
        {
            RuleFor(x => x.AuctionId)
                .NotNull();
        }
    }
}