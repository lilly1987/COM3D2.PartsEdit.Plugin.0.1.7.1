rmdir /S /q obj
"C:\Windows\Microsoft.NET\Framework\v3.5\csc" /out:COM3D2.PartsEdit.Plugin.dll ^
/t:library /lib:"F:\COM3D2\COM3D2x64_Data\Managed","G:\com3d2\_plugin\cm3d2_j_154\Sybaris" ^
/r:Assembly-CSharp.dll ^
/r:ExIni.dll ^
/r:UnityEngine.dll ^
/r:UnityInjector.dll ^
/recurse:*.cs
rem pause 
rem /r:COM3D2.API.dll ^
rem /r:System.Threading.dll ^
rem /r:0Harmony.dll ^
rem /r:Assembly-CSharp-firstpass.dll ^
rem /r:BepInEx.dll ^
rem ,"F:\COM3D2\BepInEx\plugins","F:\COM3D2\BepInEx\core"