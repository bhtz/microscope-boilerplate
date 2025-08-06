using MediatR;

namespace Microscope.Boilerplate.Framework.CQRS;

public interface IQuery<T> : IRequest<T>
{

}