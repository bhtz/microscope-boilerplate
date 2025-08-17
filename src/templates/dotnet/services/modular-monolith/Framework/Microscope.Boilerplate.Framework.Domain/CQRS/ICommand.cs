using MediatR;

namespace Microscope.Boilerplate.Framework.Domain.CQRS;

public interface ICommand<T> : IRequest<T>
{

}