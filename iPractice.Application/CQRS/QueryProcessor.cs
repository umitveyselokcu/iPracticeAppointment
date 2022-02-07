using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Query;
using MediatR;

namespace iPractice.Application.CQRS
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IMediator _mediator;

        public QueryProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task ProcessAsync(Expression command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}