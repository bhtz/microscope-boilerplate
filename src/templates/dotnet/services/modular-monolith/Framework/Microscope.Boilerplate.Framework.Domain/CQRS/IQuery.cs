using MediatR;

namespace Microscope.Framework.Domain.CQRS;

public interface IQuery<T> : IRequest<T>
{

}