using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Handler;
using iPractice.DataAccess;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Queries.GetPsychologists
{
    public class GetPsychologistsQueryHandler : IQueryHandler<GetPsychologistsQuery, List<Psychologist>>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public GetPsychologistsQueryHandler(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Psychologist>> Handle(GetPsychologistsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<Psychologist>().CreateQuery()
                .Include(x => x.Clients)
                .Include(x => x.AppointmentSlots)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}