namespace Microscope.Boilerplate.Framework.Domain.DDD;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
{
    public DateTimeOffset CreatedAt { get; set; }
    public TId CreatedBy { get; set; } = default!;
    public string? CreatorMail { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; } = default!;
}

public abstract class AuditableEntity : AuditableEntity<Guid>, IAuditableEntity, IEntity
{
}
