using FluentValidation;
using iPractice.Abstraction.Validation;
using iPractice.Application.Commands.CreateAvailability;

namespace iPractice.Application.Commands.UpdateAvailability
{
    public class UpdateAvailabilityCommandValidator: BaseValidator<UpdateAvailabilityCommand>
    {
    public UpdateAvailabilityCommandValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.PsychologistId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.AvailabilitySlotId).NotEmpty().GreaterThan(0);
    }
    }
}