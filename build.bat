@echo off
REM بناء ملف exe مستقل واحد لويندوز (يتطلّب .NET 8 SDK)
dotnet publish src\ExamBuilder.App\ExamBuilder.App.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -o publish
echo.
echo تم إنشاء الملف في: publish\ExamBuilder.exe
pause
