namespace ExamBuilder.Domain.Entities;

public class AnswerOption
{
    public int Id { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public string Label { get; set; } = string.Empty;   // أ | ب | ج | د
    public string Content { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
