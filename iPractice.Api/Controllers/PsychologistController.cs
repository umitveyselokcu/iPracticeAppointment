using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iPractice.ApiBase.ApiBase;
using iPractice.ApiBase.Response;
using iPractice.Application.Commands.CreateAvailability;
using iPractice.Application.Commands.UpdateAvailability;
using iPractice.Application.CQRS;
using iPractice.Application.Queries.GetPsychologists;
using iPractice.DataAccess.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iPractice.Api.Controllers
{
    public class PsychologistController : ApiControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IQueryProcessor _queryProcessor;
        
        public PsychologistController(ICommandSender commandSender, IQueryProcessor queryProcessor)
        {
            _commandSender = commandSender;
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<Response<List<Psychologist>>> Get()
        {
            var data = await _queryProcessor.ProcessAsync(new GetPsychologistsQuery()).ConfigureAwait(false);
            return ProduceResponse(data);
        }

        /// <summary>
        /// Add a block of time during which the psychologist is available during normal business hours
        /// </summary>
        /// <param name="psychologistId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>Ok if the availability was created</returns>
        [HttpPost("{psychologistId}/availability")]
        public async Task<Response<Unit>> CreateAvailability([FromRoute] long psychologistId, [FromBody] CreateAvailabilityCommand command)
        {
            command.PsychologistId = psychologistId;
            await _commandSender.SendAsync(command)
                .ConfigureAwait(false);
            return ProduceResponse(Unit.Value);
        }

        /// <summary>
        /// Update availability of a psychologist
        /// </summary>
        /// <param name="psychologistId">The psychologist's ID</param>
        /// <param name="availabilitySlotId"></param>
        /// <param name="newDate"></param>
        /// <returns>List of availability slots</returns>
        [HttpPut("{psychologistId}/availability/{availabilitySlotId}")]
        public async Task<Response<Unit>> UpdateAvailability([FromRoute] long psychologistId, [FromRoute] long availabilitySlotId, [FromBody] DateTime newDate)
        {
            await _commandSender.SendAsync(new UpdateAvailabilityCommand(availabilitySlotId, psychologistId, newDate))
                .ConfigureAwait(false);
            return ProduceResponse(Unit.Value);
        }
    }
}
