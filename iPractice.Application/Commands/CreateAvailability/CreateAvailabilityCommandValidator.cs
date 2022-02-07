using FluentValidation;
using iPractice.Abstraction.Validation;

namespace iPractice.Application.Commands.CreateAvailability
{
    public class CreateAvailabilityCommandValidator: BaseValidator<CreateAvailabilityCommand>
    {
        public CreateAvailabilityCommandValidator()
        {
            RuleFor(x => x.FromDate).NotEmpty();
            RuleFor(x => x.ToDate).NotEmpty();
            RuleFor(x => x.ToDate).Must((c,toDate) => c.FromDate.Ticks < toDate.Ticks);
        }
    }
}