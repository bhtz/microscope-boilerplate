using MediatR;

namespace Microscope.SharedKernel;

public abstract class DomainEvent : IEvent
{
    public DateTime CreatedAt { get; set; }
}