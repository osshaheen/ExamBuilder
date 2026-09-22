using ExamBuilder.Domain.Enums;

namespace ExamBuilder.Domain.Entities;

public class Exam
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int AcademicYearId { get; set; }
    public AcademicYear AcademicYear { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    // بيانات الترويسة الموزّعة
    public string MinistryName { get; set; } = "وزارة التربية والتعليم";
    public string CenterName { get; set; } = "المركز الوطني للامتحانات";
    public string? Branch { get; set; }
    public string? Session { get; set; }
    public DateTime? ExamDate { get; set; }
    public string? Duration { get; set; }
    public int TotalMark { get; set; } = 100;
    public string? Instructions { get; set; }
    public string ClosingPhrase { get; set; } = "انتهت الأسئلة";
    public string? LogoPath { get; set; }
    public bool PageNumbering { get; set; } = true;

    public ExamStatus Status { get; set; } = ExamStatus.Draft;

    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
