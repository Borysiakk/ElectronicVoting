Import-Module SqlServer

cd $PSScriptRoot/..
Write-Host $pwd;

$connectionString = "Server=localhost,8090;User Id=sa;Password=LitwoOjczyznoMoja1234@;TrustServerCertificate=true"

# Connect to the database
$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = $connectionString
$connection.Open()

$command = $connection.CreateCommand()
$command.CommandText = "SELECT * FROM dbo.Validators"
$reader = $command.ExecuteReader()

Invoke-Expression "dotnet ef migrations add Init --context ApplicationDbContext"
while ($reader.Read())
{
    $connectionStringValidator = $reader.GetValue($reader.GetOrdinal("ConnectionStringToBuild"));
    Invoke-Expression "dotnet ef database update --context ApplicationDbContext --connection ""$connectionStringValidator""";
}