using iPractice.Abstraction.Query;
using MediatR;

namespace iPractice.Abstraction.Handler
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}