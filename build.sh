MONO_SDK=$(pwd)/mono-wasm-sdk

platform="$(uname -s)"
case "${platform}" in
	MINGW*) ;&
	MSYS*)     buildCmd="dotnet build";;
	*)          buildCmd="msbuild";;
esac


eval $buildCmd Inzynierka.sln
mono $MONO_SDK/packager.exe -prefix=FactorialTask/bin/Debug -out=client FactorialTask.dll
