using ExamBuilder.Domain.Enums;

namespace ExamBuilder.Domain.Entities;

public class Activity
{
    public int Id { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public ActivityAction ActionType { get; set; }
    public string? EntityType { get; set; }   // exam | question | user...
    public int? EntityId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
