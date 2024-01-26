@echo off
dotnet build src/Limbo.Umbraco.Seo --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget