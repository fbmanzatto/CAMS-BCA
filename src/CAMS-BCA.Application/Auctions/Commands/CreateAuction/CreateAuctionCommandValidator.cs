using FluentValidation;

namespace CAMS_BCA.Application.Auctions.Commands.CreateAuction
{
    public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
    {
        public CreateAuctionCommandValidator()
        {
            RuleFor(x => x.Description)
                .MinimumLength(2)
                .MaximumLength(10000);
        }
    }
}