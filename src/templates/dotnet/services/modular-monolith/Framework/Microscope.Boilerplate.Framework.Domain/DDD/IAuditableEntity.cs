namespace Microscope.Boilerplate.Framework.Domain.DDD;

public interface IAuditableEntity<TId> : IEntity<TId>
{
    public DateTimeOffset CreatedAt { get; set; }
    public TId CreatedBy { get; set; }
    public string? CreatorMail { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; }
}

public interface IAuditableEntity : IAuditableEntity<Guid>, IEntity
{
}