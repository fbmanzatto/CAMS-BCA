using FluentValidation;

namespace CAMS_BCA.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateTruckVehicleCommandValidator : AbstractValidator<CreateTruckVehicleCommand>
    {
        public CreateTruckVehicleCommandValidator()
        {
            RuleFor(x => x.UniqueIdentifier)
                .MinimumLength(2)
                .MaximumLength(10000);

            RuleFor(x => x.Model)
                .MinimumLength(2)
                .MaximumLength(10000);

            RuleFor(x => x.Manufacturer)
                .MinimumLength(2)
                .MaximumLength(10000);

            RuleFor(x => x.Year)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .GreaterThanOrEqualTo(1950);

            RuleFor(x => x.StartingBid)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.LoadCapacity)
                .GreaterThanOrEqualTo(1);
        }
    }
}