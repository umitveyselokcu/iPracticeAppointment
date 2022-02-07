using System;
using System.Collections.Generic;

namespace iPractice.Abstraction.DTO
{
    public class Availability
    {
        public long PsychologistId { get; set; }
        public string PsychologistName { get; set; }
        public IEnumerable<DateTime> AvailableSlots = new List<DateTime>();
    }
}