using System.Collections.ObjectModel;
using ExamBuilder.App.Infrastructure;
using ExamBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamBuilder.App.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    public int ExamCount { get; }
    public int QuestionCount { get; }
    public int SubjectCount { get; }
    public int UserCount { get; }

    public ObservableCollection<ExamRow> RecentExams { get; } = new();

    public DashboardViewModel()
    {
        using var db = new AppDbContext();
        ExamCount = db.Exams.Count();
        QuestionCount = db.Questions.Count();
        SubjectCount = db.Subjects.Count();
        UserCount = db.Users.Count();

        var recent = db.Exams
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .OrderByDescending(e => e.UpdatedAt)
            .Take(6)
            .ToList();

        foreach (var e in recent)
        {
            RecentExams.Add(new ExamRow(
                e.Title,
                $"{e.Questions.Count} سؤالاً · العلامة {e.TotalMark}",
                e.Status == Domain.Enums.ExamStatus.Ready ? "جاهز" : "مسودّة"));
        }
    }

    public record ExamRow(string Title, string Meta, string Status);
}
