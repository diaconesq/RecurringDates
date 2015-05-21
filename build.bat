rem @echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

if "%NuGet%" == "" (
   set NuGet=.nuget\nuget.exe
)

if "%MsBuildExe%" == "" (
   set MsBuildExe=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild
)

REM Package restore
"%NuGet%" restore RecurringDates.sln -OutputDirectory "%cd%\packages" -NonInteractive

REM Build
"%MsBuildExe%" RecurringDates.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Dynamically restore the required test runner
"%NuGet%" install NUnit.Runners -Version 2.6.4 -OutputDirectory packages
set NUnitExe="%cd%\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe"

REM Test

"%NUnitExe%" "%cd%\RecurringDates.UnitTests\bin\%config%\RecurringDates.UnitTests.dll"
