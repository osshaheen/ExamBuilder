using System.Windows;
using ExamBuilder.Data;
using ExamBuilder.Domain.Entities;
using ExamBuilder.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExamBuilder.App.Windows;

public partial class LoginWindow : Window
{
    public User? AuthenticatedUser { get; private set; }

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var email = EmailBox.Text.Trim();
        var password = PasswordBox.Password;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ShowError("يرجى إدخال البريد الإلكتروني وكلمة المرور.");
            return;
        }

        using var db = new AppDbContext();
        var user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);

        if (user is null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            ShowError("بيانات الدخول غير صحيحة.");
            return;
        }

        if (user.Status != UserStatus.Active)
        {
            ShowError("هذا الحساب غير مُفعّل. تواصل مع مدير النظام.");
            return;
        }

        user.LastActiveAt = DateTime.Now;
        db.Activities.Add(new Activity
        {
            UserId = user.Id,
            ActionType = ActivityAction.Login,
            Description = "تسجيل الدخول إلى النظام"
        });
        db.SaveChanges();

        AuthenticatedUser = user;
        DialogResult = true;
        Close();
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.Visibility = Visibility.Visible;
    }
}
