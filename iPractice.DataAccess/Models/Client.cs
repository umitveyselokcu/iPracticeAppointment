using System.Collections.Generic;

namespace iPractice.DataAccess.Models
{
    public class Client : Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Psychologist> Psychologists { get; set; }
        
        public ICollection<AppointmentSlot> Appointments { get; set; }
    }
}