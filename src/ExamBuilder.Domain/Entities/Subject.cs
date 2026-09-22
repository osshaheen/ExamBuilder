namespace ExamBuilder.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Branch { get; set; }   // علمي | أدبي | مشترك
    public string? Code { get; set; }

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
