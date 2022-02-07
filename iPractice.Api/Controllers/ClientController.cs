using System.Collections.Generic;
using System.Threading.Tasks;
using iPractice.Abstraction.DTO;
using iPractice.ApiBase.ApiBase;
using iPractice.ApiBase.Response;
using iPractice.Application.Commands.CreateAppointment;
using iPractice.Application.CQRS;
using iPractice.Application.Queries.GetAvailableTimeSlots;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iPractice.Api.Controllers
{
    public class ClientController : ApiControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IQueryProcessor _queryProcessor;
        
        public ClientController(ICommandSender commandSender, IQueryProcessor queryProcessor)
        {
            _commandSender = commandSender;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        public async Task<Response<List<Availability>>> GetAvailableTimeSlots([FromRoute]long clientId)
        {
            var data = await _queryProcessor.ProcessAsync(new GetAvailableTimeSlotsQuery(clientId));
            return ProduceResponse(data);
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="appointmentSlotId"></param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        public async Task<Response<Unit>> CreateAppointment([FromRoute]long clientId, [FromBody]long appointmentSlotId)
        {
            var data = await _commandSender.SendAsync(new CreateAppointmentCommand(clientId, appointmentSlotId));
            return ProduceResponse(data);
        }
    }
}
