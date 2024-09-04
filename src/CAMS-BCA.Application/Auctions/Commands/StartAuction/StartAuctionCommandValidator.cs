using FluentValidation;

namespace CAMS_BCA.Application.Auctions.Commands.StartAuction
{
    public class StartAuctionCommandValidator : AbstractValidator<StartAuctionCommand>
    {
        public StartAuctionCommandValidator()
        {
            RuleFor(x => x.AuctionId)
                .NotNull();
        }
    }
}