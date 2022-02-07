using MediatR;

namespace iPractice.Abstraction.Query
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
