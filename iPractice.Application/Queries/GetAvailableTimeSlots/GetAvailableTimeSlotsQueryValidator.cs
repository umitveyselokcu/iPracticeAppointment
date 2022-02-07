using FluentValidation;
using iPractice.Abstraction.Query;
using iPractice.Abstraction.Validation;
using iPractice.Application.Queries.GetAvailableTimeSlots;

namespace iPractice.Application.Validators
{
    public class GetAvailableTimeSlotsQueryValidator : BaseValidator<GetAvailableTimeSlotsQuery>
    {
        public GetAvailableTimeSlotsQueryValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty().GreaterThan(0);
        }
    }
}