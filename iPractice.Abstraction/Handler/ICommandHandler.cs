using iPractice.Abstraction.Command;
using MediatR;

namespace iPractice.Abstraction.Handler
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
