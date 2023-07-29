using MediatR;

namespace Microscope.SharedKernel;

public interface ICommand<T> : IRequest<T>
{

}