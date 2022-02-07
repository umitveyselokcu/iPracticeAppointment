using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Command;
using iPractice.DataAccess;
using iPractice.DataAccess.UnitOfWork;
using MediatR.Pipeline;

namespace iPractice.Application.ProcessorPipeline
{
    public class RequestPostProcessor<TCommand, TResponse> : IRequestPostProcessor<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public RequestPostProcessor(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Process(TCommand request, TResponse response, CancellationToken cancellationToken)
        {
            await _unitOfWork.Complete();
        }
    }
}