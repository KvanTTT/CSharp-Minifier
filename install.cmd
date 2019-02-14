@echo off
NuGet install Microsoft.Net.Compilers -Version 2.10.0 -OutputDirectory "%~dp0tools" ^
   && ren "%~dp0tools\Microsoft.Net.Compilers.2.10.0" Microsoft.Net.Compilers
