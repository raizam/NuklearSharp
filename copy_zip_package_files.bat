rem delete existing
rmdir "ZipPackage" /Q /S

rem Create required folders
mkdir "ZipPackage"
mkdir "ZipPackage\netstandard1.1"
mkdir "ZipPackage\Samples"
mkdir "ZipPackage\Samples\MonoGame"
mkdir "ZipPackage\Samples\MonoGame\Content"

set "CONFIGURATION=Release"

rem Copy output files
copy "NuklearSharp\bin\%CONFIGURATION%\NuklearSharp.dll" ZipPackage /Y
copy "NuklearSharp\bin\%CONFIGURATION%\netstandard1.1\publish\NuklearSharp.dll" ZipPackage\netstandard1.1 /Y
copy "NuklearSharp.MonoGame\bin\%CONFIGURATION%\NuklearSharp.MonoGame.dll" ZipPackage /Y

rem Firstly copy Extended sample with all stuff and content
xcopy /S /E "Samples\Extended\bin\%CONFIGURATION%\*.*" ZipPackage\Samples\MonoGame\
del ZipPackage\Samples\MonoGame\*vshost*
del ZipPackage\Samples\MonoGame\*.pdb

copy "Samples\RaizamTest\bin\%CONFIGURATION%\RaizamTest.exe" ZipPackage\Samples\MonoGame /Y
copy "Samples\RaizamTest\bin\%CONFIGURATION%\RaizamTest.exe.config" ZipPackage\Samples\MonoGame /Y
