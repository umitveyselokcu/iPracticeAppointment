using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Handler;
using iPractice.Application.Commands.CreateAvailability;
using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Commands.UpdateAvailability
{
    public class UpdateAvailabilityCommandHandler : ICommandHandler<UpdateAvailabilityCommand, Unit>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        
        public UpdateAvailabilityCommandHandler(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var psychologist = await _unitOfWork.GetRepository<Psychologist>().CreateQuery().Include(x => x.AppointmentSlots)
                .Where(x => x.Id == request.PsychologistId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


            var appointmentSlot = psychologist.AppointmentSlots.FirstOrDefault(x => x.Id == request.AvailabilitySlotId);
            if (appointmentSlot != null) appointmentSlot.TimeSlot = request.StartDate;
            return Unit.Value;
        }
    }
}