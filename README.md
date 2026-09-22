# مُنشئ الامتحانات — ExamBuilder

تطبيق سطح مكتب (Native WPF / .NET 8) لإعداد وتصدير الامتحانات المدرسية، يعمل دون اتصال، مع قاعدة بيانات SQLite.

## البنية
- `src/ExamBuilder.Domain` — الكيانات والتعدادات (نموذج البيانات).
- `src/ExamBuilder.Data` — طبقة البيانات: `AppDbContext` (EF Core + SQLite)، تجزئة كلمات المرور، وزرع البيانات الأولية.
- `src/ExamBuilder.App` — واجهة WPF (نوافذ، شاشات، MVVM).

## الحصول على ملف exe جاهز (عبر GitHub — بلا حاجة لويندوز)
1. أنشئ مستودعاً على GitHub وارفع هذا المجلد إليه.
2. يعمل `.github/workflows/build.yml` تلقائياً عند الدفع إلى `main`.
3. من تبويب **Actions** → آخر تشغيل → قسم **Artifacts** → نزّل `ExamBuilder-win-x64` (بداخله `ExamBuilder.exe`).

يمكن أيضاً تشغيل البناء يدوياً من **Actions → build-windows-exe → Run workflow**.

## البناء محلياً على ويندوز (اختياري)
يتطلّب .NET 8 SDK، ثم:
```
build.bat
```
الناتج: `publish\ExamBuilder.exe` (ملف واحد مستقل يعمل دون تثبيت .NET).

## بيانات دخول تجريبية
- البريد: `maryam@moe.gov`
- كلمة المرور: `123456`

> قاعدة البيانات تُنشأ تلقائياً في `%AppData%\ExamBuilder\exam.db` عند أول تشغيل.
