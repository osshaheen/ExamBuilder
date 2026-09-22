using ExamBuilder.Domain.Enums;

namespace ExamBuilder.Domain.Entities;

public class Question
{
    public int Id { get; set; }

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public int OrderNo { get; set; }
    public string Stem { get; set; } = string.Empty;     // نص/HTML منسّق
    public string? StemOmml { get; set; }                 // معادلات Office
    public QuestionLayout Layout { get; set; } = QuestionLayout.Vertical;
    public int Mark { get; set; } = 1;

    public ICollection<AnswerOption> Options { get; set; } = new List<AnswerOption>();
}
