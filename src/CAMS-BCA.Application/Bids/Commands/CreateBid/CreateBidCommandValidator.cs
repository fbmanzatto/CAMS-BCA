using FluentValidation;

namespace CAMS_BCA.Application.Bids.Commands.CreateBid
{
    public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
    {
        public CreateBidCommandValidator()
        {
            RuleFor(x => x.VehicleId)
                .NotNull();

            RuleFor(x => x.AuctionId)
                .NotNull();

            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(1);
        }
    }
}