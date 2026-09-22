using System.Windows;
using ExamBuilder.App.ViewModels;
using ExamBuilder.Domain.Entities;

namespace ExamBuilder.App.Windows;

public partial class MainWindow : Window
{
    public MainWindow(User user)
    {
        InitializeComponent();
        DataContext = new MainViewModel(user);
    }
}
