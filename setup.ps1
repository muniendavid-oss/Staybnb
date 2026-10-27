$ErrorActionPreference = "Stop"
Write-Host "Checking .NET SDK..."
dotnet --version
Write-Host "Restoring packages..."
dotnet restore
if (-not (Get-Command dotnet-ef -ErrorAction SilentlyContinue)) {
  dotnet tool install --global dotnet-ef --version 8.0.11
}
Write-Host "Creating the initial EF Core migration..."
dotnet ef migrations add InitialCreate
Write-Host "Creating/updating the SQL Server database..."
dotnet ef database update
Write-Host "Setup complete. Start the app with: dotnet run"
