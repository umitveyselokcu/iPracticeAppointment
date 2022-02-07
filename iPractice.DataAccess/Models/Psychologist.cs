using System.Collections.Generic;

namespace iPractice.DataAccess.Models
{
    public class Psychologist : Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<AppointmentSlot> AppointmentSlots { get; set; }
    }
}