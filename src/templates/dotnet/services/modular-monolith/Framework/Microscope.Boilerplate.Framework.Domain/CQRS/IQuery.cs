using MediatR;

namespace Microscope.Boilerplate.Framework.Domain.CQRS;

public interface IQuery<T> : IRequest<T>
{

}