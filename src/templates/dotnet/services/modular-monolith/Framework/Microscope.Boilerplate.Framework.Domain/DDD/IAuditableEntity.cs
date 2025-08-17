namespace Microscope.Boilerplate.Framework.Domain.DDD;

public interface IAuditableEntity<TId>
{
    public DateTime CreatedAt { get; set; }
    public TId CreatedBy { get; set; }
    public string? CreatorMail { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; }
}

public interface IAuditableEntity : IAuditableEntity<Guid>
{
    
}