using MediatR;

namespace Microscope.SharedKernel;

public interface IQuery<T> : IRequest<T>
{

}