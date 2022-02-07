using System;
using iPractice.Abstraction.Command;
using MediatR;

namespace iPractice.Application.Commands.UpdateAvailability
{
    public class UpdateAvailabilityCommand : CommandBase<Unit>
    {
        public UpdateAvailabilityCommand(long availabilitySlotId, long psychologistId, DateTime startDate)
        {
            AvailabilitySlotId = availabilitySlotId;
            StartDate = startDate;
            PsychologistId = psychologistId;
        }
        public long AvailabilitySlotId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public long PsychologistId { get; set; }
    }
}