using MediatR;

namespace Microscope.SharedKernel;

public abstract class DomainEvent : INotification
{
    public DateTime CreatedAt { get; set; }
}