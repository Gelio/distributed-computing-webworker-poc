MONO_SDK=$(pwd)/mono-wasm-sdk

dotnet build Inzynierka.sln
mono $MONO_SDK/packager.exe -prefix=FactorialTask/bin/Debug -out=client FactorialTask.dll
