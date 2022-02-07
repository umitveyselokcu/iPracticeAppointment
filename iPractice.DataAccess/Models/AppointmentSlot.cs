using System;

namespace iPractice.DataAccess.Models
{   /// <summary>
    /// I assume minimum time slot as 30 minute
    /// </summary>
    public class AppointmentSlot : Entity
    {
        public long Id { get; set; }
        
        public DateTime TimeSlot { get; set; }
        public virtual Psychologist Psychologist { get; set; }
        public long? ClientId { get; set; }
        public void BookAppointment(long clientId)
        {
            this.ClientId = clientId;
        }
    }
}