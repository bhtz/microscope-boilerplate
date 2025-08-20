namespace Microscope.Boilerplate.Framework.Domain.DDD;

public abstract class AuditableAggregateRoot<TId> : AggregateRoot<TId>, IAuditableEntity<TId>
{
    public DateTimeOffset CreatedAt { get; set; }
    public TId CreatedBy { get; set; } = default!;
    public string? CreatorMail { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; } = default!;
}

public abstract class AuditableAggregateRoot : AuditableAggregateRoot<Guid>, IAuditableEntity
{
}
