using System.Collections.Generic;
using iPractice.Abstraction.DTO;
using iPractice.Abstraction.Query;

namespace iPractice.Application.Queries.GetAvailableTimeSlots
{
    public class GetAvailableTimeSlotsQuery : QueryBase<List<Availability>>
    {
        public GetAvailableTimeSlotsQuery(long clientId)
        {
            this.ClientId = clientId;
        }
        public long ClientId { get; set; }
    }
}