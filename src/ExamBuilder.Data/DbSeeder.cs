using ExamBuilder.Domain.Entities;
using ExamBuilder.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExamBuilder.Data;

public static class DbSeeder
{
    /// <summary>ينشئ قاعدة البيانات إن لم تكن موجودة ويزرع بيانات أولية.</summary>
    public static void EnsureSeeded(AppDbContext db)
    {
        db.Database.EnsureCreated();
        if (db.Roles.Any()) return;

        // الأدوار
        var admin = new Role { Name = "مدير النظام", Description = "صلاحيات كاملة" };
        var coordinator = new Role { Name = "مُنسّق مادة" };
        var teacher = new Role { Name = "معلّم / مُعِدّ" };
        var reviewer = new Role { Name = "مُدقّق لغوي" };
        var viewer = new Role { Name = "مشاهد" };
        db.Roles.AddRange(admin, coordinator, teacher, reviewer, viewer);

        // الصلاحيات
        string[,] perms =
        {
            {"exam.manage","إنشاء وتعديل الامتحانات"},
            {"header.manage","إدارة الترويسة والقوالب"},
            {"question.review","تدقيق ومراجعة الأسئلة"},
            {"export","تصدير Word / PDF / Excel"},
            {"year.manage","إدارة الأعوام الدراسية"},
            {"user.manage","إدارة المستخدمين والصلاحيات"},
        };
        var pList = new List<Permission>();
        for (int i = 0; i < perms.GetLength(0); i++)
            pList.Add(new Permission { Key = perms[i, 0], Name = perms[i, 1] });
        db.Permissions.AddRange(pList);
        db.SaveChanges();

        // ربط صلاحيات مدير النظام بالكل
        foreach (var p in pList)
            db.RolePermissions.Add(new RolePermission { RoleId = admin.Id, PermissionId = p.Id });

        // مستخدم افتراضي (كلمة المرور: 123456)
        var maryam = new User
        {
            FullName = "مريم عبدالله",
            Email = "maryam@moe.gov",
            PasswordHash = PasswordHasher.Hash("123456"),
            RoleId = admin.Id,
            Status = UserStatus.Active,
            LastActiveAt = DateTime.Now
        };
        db.Users.Add(maryam);

        // العام الدراسي الحالي
        var year = new AcademicYear
        {
            Name = "2025/2026",
            StartDate = new DateTime(2025, 9, 1),
            EndDate = new DateTime(2026, 6, 30),
            IsCurrent = true,
            Status = YearStatus.Active
        };
        db.AcademicYears.Add(year);

        // المواد
        var math = new Subject { Name = "الرياضيات", Branch = "علمي", Code = "MATH" };
        var physics = new Subject { Name = "الفيزياء", Branch = "علمي", Code = "PHY" };
        var arabic = new Subject { Name = "اللغة العربية", Branch = "مشترك", Code = "AR" };
        db.Subjects.AddRange(math, physics, arabic);
        db.SaveChanges();

        // امتحان نموذجي للرياضيات
        var exam = new Exam
        {
            SubjectId = math.Id,
            AcademicYearId = year.Id,
            Title = "امتحان الرياضيات — الفرع العلمي",
            Branch = "العلمي",
            Session = "الصباحية",
            ExamDate = new DateTime(2026, 9, 20),
            Duration = "ساعتان",
            TotalMark = 100,
            Instructions = "يتكوّن الامتحان من (١٢) سؤالاً موضوعياً. ظلّل دائرة الإجابة الصحيحة، ولكل سؤال إجابة واحدة صحيحة فقط.",
            Status = ExamStatus.Ready,
            CreatedById = maryam.Id
        };
        exam.Questions.Add(new Question
        {
            OrderNo = 1,
            Stem = "ما قيمة (س) التي تحقّق المعادلة: ٢س + ٥ = ١٧ ؟",
            Layout = QuestionLayout.Horizontal,
            Mark = 4,
            Options = new List<AnswerOption>
            {
                new() { Label = "أ", Content = "٤", IsCorrect = false },
                new() { Label = "ب", Content = "٥", IsCorrect = false },
                new() { Label = "ج", Content = "٦", IsCorrect = true },
                new() { Label = "د", Content = "٧", IsCorrect = false },
            }
        });
        db.Exams.Add(exam);

        db.Activities.Add(new Activity
        {
            UserId = maryam.Id,
            ActionType = ActivityAction.Create,
            EntityType = "exam",
            Description = "أنشأت امتحان الرياضيات — العلمي"
        });

        db.SaveChanges();
    }
}
