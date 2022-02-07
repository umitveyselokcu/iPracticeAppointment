using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using iPractice.Abstraction.Exceptions;
using iPractice.Abstraction.Handler;
using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, Unit>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        
        public CreateAppointmentCommandHandler(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.GetRepository<AppointmentSlot>()
                .GetFirstAsync(x => x.Id == request.AppointmentSlotId, cancellationToken).ConfigureAwait(false);
            
            if(appointment == null)
            {
                throw new BusinessException($"AppointmentSlot does not exist. AppointmentId: {request.AppointmentSlotId} ");
            }

            if (appointment.ClientId != null)
            {
                throw new BusinessException($"Slot has already taken. Please select another available one.");
            }
            
            var appointmentAtTheSameTime = await _unitOfWork.GetRepository<Client>().CreateQuery().Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken).ConfigureAwait(false);

            if ((bool) appointmentAtTheSameTime?.Appointments?.Any(x=> x.TimeSlot == appointment.TimeSlot))
            {
                throw new BusinessException($"You already have an appointment at the same time slot. Please select another available one.");
            }

            appointment.BookAppointment(request.ClientId);
            
            return Unit.Value;
        }
    }
}