using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Handler;
using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.UnitOfWork;
using MediatR;

namespace iPractice.Application.Commands.CreateAvailability
{
    public class CreateAvailabilityCommandHandler : ICommandHandler<CreateAvailabilityCommand, Unit>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        
        public CreateAvailabilityCommandHandler(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var psychologist = await _unitOfWork.GetRepository<Psychologist>()
                .GetFirstAsync(x => x.Id == request.PsychologistId, cancellationToken).ConfigureAwait(false);
            var slots = new List<AppointmentSlot>();
            var startDate = request.FromDate;
    
            while (startDate < request.ToDate)
            {
                slots.Add(new AppointmentSlot()
                {
                    ClientId = (long?) null,
                    Psychologist = psychologist,
                    TimeSlot = startDate
                });
                startDate = startDate.AddMinutes(30);
            }
            
            _unitOfWork.GetRepository<AppointmentSlot>().AddRange(slots);
            
            return Unit.Value;
        }
    }
}