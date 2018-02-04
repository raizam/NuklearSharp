rem delete existing
rmdir "ZipPackage" /Q /S

rem Create required folders
mkdir "ZipPackage"
mkdir "ZipPackage\netstandard1.1"

set "CONFIGURATION=Release"

rem Copy output files
copy "NuklearSharp\bin\%CONFIGURATION%\NuklearSharp.dll" ZipPackage /Y
copy "NuklearSharp\bin\%CONFIGURATION%\netstandard1.1\publish\NuklearSharp.dll" ZipPackage\netstandard1.1 /Y
copy "NuklearSharp.MonoGame\bin\%CONFIGURATION%\NuklearSharp.MonoGame.dll" ZipPackage /Y