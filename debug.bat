@echo off
dotnet build src/Limbo.Umbraco.Seo --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:\nuget\Umbraco8