using System.Collections.ObjectModel;
using System.Windows.Input;
using ExamBuilder.App.Infrastructure;
using ExamBuilder.Domain.Entities;

namespace ExamBuilder.App.ViewModels;

public class MainViewModel : ViewModelBase
{
    public User CurrentUser { get; }
    public string UserName => CurrentUser.FullName;
    public string RoleName => CurrentUser.Role?.Name ?? "";
    public string Initials
    {
        get
        {
            var parts = CurrentUser.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 2 ? $"{parts[0][0]}.{parts[1][0]}" : CurrentUser.FullName[..1];
        }
    }

    public ObservableCollection<NavItem> WorkspaceItems { get; }
    public ObservableCollection<NavItem> AdminItems { get; }

    private object? _current;
    public object? Current
    {
        get => _current;
        set => SetProperty(ref _current, value);
    }

    public ICommand NavigateCommand { get; }

    public MainViewModel(User user)
    {
        CurrentUser = user;

        WorkspaceItems = new ObservableCollection<NavItem>
        {
            new("dashboard", "لوحة التحكم"),
            new("header", "ترويسة الامتحان"),
            new("questions", "بناء الأسئلة"),
            new("export", "التصدير والمخرجات"),
        };
        AdminItems = new ObservableCollection<NavItem>
        {
            new("years", "الأعوام الدراسية"),
            new("exams", "الامتحانات"),
            new("users", "المستخدمون والصلاحيات"),
            new("activities", "الأنشطة"),
        };

        NavigateCommand = new RelayCommand(p => Navigate(p as string));
        Navigate("dashboard");
    }

    private void Navigate(string? key)
    {
        if (string.IsNullOrEmpty(key)) return;

        foreach (var item in WorkspaceItems) item.IsActive = item.Key == key;
        foreach (var item in AdminItems) item.IsActive = item.Key == key;

        Current = key switch
        {
            "dashboard" => new DashboardViewModel(),
            "header" => new PlaceholderViewModel("ترويسة الامتحان", "إدخال بيانات رأس الصفحة والشعار والتذييل."),
            "questions" => new PlaceholderViewModel("بناء الأسئلة", "محرّر الأسئلة الموضوعية والخيارات والإجابة الصحيحة."),
            "export" => new PlaceholderViewModel("التصدير والمخرجات", "تصدير Word و PDF و مفتاح إجابة Excel."),
            "years" => new PlaceholderViewModel("الأعوام الدراسية", "تعريف الأعوام الدراسية وتحديد العام الحالي."),
            "exams" => new PlaceholderViewModel("الامتحانات", "امتحان لكل مادة دراسية لكل عام."),
            "users" => new PlaceholderViewModel("المستخدمون والصلاحيات", "إدارة الحسابات والأدوار والصلاحيات."),
            "activities" => new PlaceholderViewModel("الأنشطة", "سجلّ كل ما جرى في النظام."),
            _ => Current
        };
    }
}
