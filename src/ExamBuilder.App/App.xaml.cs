using System.Windows;
using ExamBuilder.Data;
using ExamBuilder.Domain.Entities;
using ExamBuilder.App.Windows;

namespace ExamBuilder.App;

public partial class App : Application
{
    /// <summary>المستخدم المسجّل حالياً (يُضبط بعد نجاح الدخول).</summary>
    public static User? CurrentUser { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // إنشاء قاعدة البيانات وزرعها إن لزم
        using (var db = new AppDbContext())
            DbSeeder.EnsureSeeded(db);

        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var login = new LoginWindow();
        if (login.ShowDialog() == true && login.AuthenticatedUser is not null)
        {
            CurrentUser = login.AuthenticatedUser;
            var main = new MainWindow(CurrentUser);
            MainWindow = main;
            main.Closed += (_, _) => Shutdown();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            main.Show();
        }
        else
        {
            Shutdown();
        }
    }
}
