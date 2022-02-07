using FluentValidation;
using iPractice.Abstraction.Validation;

namespace iPractice.Application.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator : BaseValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentSlotId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ClientId).NotEmpty().GreaterThan(0);
        }
    }
}