using ExamBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamBuilder.Data;

public class AppDbContext : DbContext
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<AnswerOption> Options => Set<AnswerOption>();
    public DbSet<Activity> Activities => Set<Activity>();

    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>مسار قاعدة البيانات داخل مجلد بيانات التطبيق للمستخدم.</summary>
    public static string DbPath
    {
        get
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ExamBuilder");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "exam.db");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<RolePermission>().HasKey(x => new { x.RoleId, x.PermissionId });

        b.Entity<User>().HasIndex(x => x.Email).IsUnique();
        b.Entity<User>().Property(x => x.Status).HasConversion<string>();

        b.Entity<Role>().HasIndex(x => x.Name).IsUnique();
        b.Entity<Permission>().HasIndex(x => x.Key).IsUnique();

        b.Entity<AcademicYear>().HasIndex(x => x.Name).IsUnique();
        b.Entity<AcademicYear>().Property(x => x.Status).HasConversion<string>();

        b.Entity<Subject>().HasIndex(x => x.Code).IsUnique();

        // قاعدة: امتحان واحد لكل مادة لكل عام دراسي
        b.Entity<Exam>().HasIndex(x => new { x.SubjectId, x.AcademicYearId }).IsUnique();
        b.Entity<Exam>().Property(x => x.Status).HasConversion<string>();
        b.Entity<Exam>()
            .HasOne(x => x.CreatedBy).WithMany(u => u.CreatedExams)
            .HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.SetNull);

        b.Entity<Question>().Property(x => x.Layout).HasConversion<string>();
        b.Entity<Question>()
            .HasOne(x => x.Exam).WithMany(e => e.Questions)
            .HasForeignKey(x => x.ExamId).OnDelete(DeleteBehavior.Cascade);

        b.Entity<AnswerOption>()
            .HasOne(x => x.Question).WithMany(q => q.Options)
            .HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Cascade);

        b.Entity<Activity>().Property(x => x.ActionType).HasConversion<string>();
    }
}
