namespace Microscope.Boilerplate.Framework.EventSourcing;

public class AuditableEntity<TId>
{
    public DateTime CreatedAt { get; set; }
    public TId CreatedBy { get; set; }
    public string? CreatorMail { get; set; } = default;
    public DateTime UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; }
}
