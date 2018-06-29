cd client
xbuild ../Inzynierka.sln
mono $MONO_SDK/packager.exe ../FactorialTask/bin/Debug/FactorialTask.dll
