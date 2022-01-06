@echo off
dotnet build src/Limbo.Umbraco.Seo --configuration Debug /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=c:/nuget