using iPractice.Abstraction.Command;
using MediatR;

namespace iPractice.Application.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : CommandBase<Unit>
    {
        public CreateAppointmentCommand(long clientId, long appointmentSlotId)
        {
            this.AppointmentSlotId = appointmentSlotId;
            this.ClientId = clientId;
        }
        public long AppointmentSlotId { get; set; }
        public long ClientId { get; set; }
    }
}