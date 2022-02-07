using System;
using iPractice.Abstraction.Command;
using MediatR;

namespace iPractice.Application.Commands.CreateAvailability
{
    public class CreateAvailabilityCommand : CommandBase<Unit>
    {
        public CreateAvailabilityCommand(long psychologistId, DateTime fromDate, DateTime toDate)
        {
            this.PsychologistId = psychologistId;
            this.FromDate = fromDate;
            this.ToDate = toDate;
        }
        public long PsychologistId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}