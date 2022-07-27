dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\test\TauCode.WebApi.Tests\TauCode.WebApi.Tests.csproj
dotnet test -c Release .\test\TauCode.WebApi.Tests\TauCode.WebApi.Tests.csproj

nuget pack nuget\TauCode.WebApi.nuspec