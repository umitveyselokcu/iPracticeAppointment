using MediatR;

namespace iPractice.Abstraction.Command
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
