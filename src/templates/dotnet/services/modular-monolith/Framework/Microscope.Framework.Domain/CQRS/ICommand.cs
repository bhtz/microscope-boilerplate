using MediatR;

namespace Microscope.Framework.Domain.CQRS;

public interface ICommand<T> : IRequest<T>
{

}