dotnet restore

dotnet build TauCode.WebApi.sln -c Debug
dotnet build TauCode.WebApi.sln -c Release

dotnet test TauCode.WebApi.sln -c Debug
dotnet test TauCode.WebApi.sln -c Release

nuget pack nuget\TauCode.WebApi.nuspec