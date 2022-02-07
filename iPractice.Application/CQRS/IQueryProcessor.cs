using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Query;

namespace iPractice.Application.CQRS
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> command, CancellationToken cancellationToken = default);

        Task ProcessAsync(Expression command, CancellationToken cancellationToken = default);

    }
}