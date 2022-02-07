using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using iPractice.Abstraction.Command;

namespace iPractice.Application.CQRS
{
    public interface ICommandSender
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

        Task SendAsync(Expression command, CancellationToken cancellationToken = default);
    }
}