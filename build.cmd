@echo off
set Build="%SYSTEMDRIVE%\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MsBuild.exe"
if exist publish rd /s /q publish
%Build% "NET40/Afx.Data.SQLite.Entity/Afx.Data.SQLite.Entity.csproj" /t:Rebuild /p:Configuration=Release
dotnet build "NETStandard2.0/Afx.Data.SQLite.Entity/Afx.Data.SQLite.Entity.csproj" -c Release 
cd publish
del /q/s *.pdb
del /q/s EntityFramework*
del /q/s System.Data.SQLite*
del /q/s Afx.Data.dll Afx.Data.Entity.dll Afx.Data.xml Afx.Data.Entity.xml Afx.Data.SQLite.Entity.dll.config
rd /s /q net40\x64
rd /s /q net40\x86
pause