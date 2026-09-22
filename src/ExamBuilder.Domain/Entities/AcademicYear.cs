using ExamBuilder.Domain.Enums;

namespace ExamBuilder.Domain.Entities;

public class AcademicYear
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;   // 2025/2026
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public YearStatus Status { get; set; } = YearStatus.Active;

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
