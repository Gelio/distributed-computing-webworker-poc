MONO_SDK=$(pwd)/mono-wasm-sdk

cd client
dotnet build ../Inzynierka.sln
mono $MONO_SDK/packager.exe ../FactorialTask/bin/Debug/FactorialTask.dll
