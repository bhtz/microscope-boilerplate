using MediatR;

namespace Microscope.Boilerplate.Framework.CQRS;

public interface ICommand<T> : IRequest<T>
{

}