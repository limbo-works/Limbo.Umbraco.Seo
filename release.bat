@echo off
REM [CHANGE: abort on a failed client build so a stale/incomplete wwwroot can't be packed] Related: debug.bat, src/Limbo.Umbraco.Seo/Limbo.Umbraco.Seo.csproj
pushd src\Limbo.Umbraco.Seo\Client
call npm ci
if errorlevel 1 (popd & exit /b 1)
call npm run build
if errorlevel 1 (popd & exit /b 1)
popd
dotnet build src/Limbo.Umbraco.Seo --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget
