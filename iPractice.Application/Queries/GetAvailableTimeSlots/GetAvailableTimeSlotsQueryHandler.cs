using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.DTO;
using iPractice.Abstraction.Exceptions;
using iPractice.Abstraction.Handler;
using iPractice.Abstraction.Query;
using iPractice.Application.CQRS;
using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Queries.GetAvailableTimeSlots
{
    public class GetAvailableTimeSlotsQueryHandler : IQueryHandler<GetAvailableTimeSlotsQuery, List<Availability>>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public GetAvailableTimeSlotsQueryHandler(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Availability>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.GetRepository<Client>().CreateQuery().Where(x => x.Id == request.ClientId).Include(x=>x.Psychologists).ThenInclude(x => x.AppointmentSlots).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (client.Psychologists == null || !client.Psychologists.Any())
            {
                throw new BusinessException("You do not have any psychologists.");
            }
            
            return client.Psychologists.Select(psychologist => new Availability()
            {
                PsychologistName = psychologist.Name,
                PsychologistId = psychologist.Id,
                AvailableSlots = psychologist.AppointmentSlots?.Select(x => x.TimeSlot)
            }).ToList();

        }
    }
}