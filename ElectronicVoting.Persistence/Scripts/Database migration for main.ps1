
cd $PSScriptRoot/..
Write-Host $pwd;

Invoke-Expression "dotnet ef migrations add Init --context MainDbContext"
Invoke-Expression "dotnet ef database update --context MainDbContext"