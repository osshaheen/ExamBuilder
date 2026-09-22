using ExamBuilder.App.Infrastructure;

namespace ExamBuilder.App.ViewModels;

public class PlaceholderViewModel : ViewModelBase
{
    public string Title { get; }
    public string Subtitle { get; }

    public PlaceholderViewModel(string title, string subtitle)
    {
        Title = title;
        Subtitle = subtitle;
    }
}
