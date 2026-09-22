using ExamBuilder.App.Infrastructure;

namespace ExamBuilder.App.ViewModels;

public class NavItem : ViewModelBase
{
    public string Key { get; }
    public string Label { get; }

    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }

    public NavItem(string key, string label)
    {
        Key = key;
        Label = label;
    }
}
