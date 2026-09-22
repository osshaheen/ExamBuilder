using ExamBuilder.Domain.Enums;

namespace ExamBuilder.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public UserStatus Status { get; set; } = UserStatus.Pending;
    public DateTime? LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public ICollection<Exam> CreatedExams { get; set; } = new List<Exam>();
}
