namespace Microscope.Boilerplate.Framework.Domain.DDD;

public abstract class AuditableEntity<TId> : IAuditableEntity<TId>, IEntity<TId>
{
    public DateTime CreatedAt { get; set; }
    public TId CreatedBy { get; set; }
    public string? CreatorMail { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; }
    public TId Id { get; }
}

public abstract class AuditableEntity : AuditableEntity<Guid>, IAuditableEntity, IEntity
{
    
}
