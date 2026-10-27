# Run-Metrics.ps1 - generate this week's code metrics for the game.
# Place this file in <Sprint-1>\tools\ next to the CodeMetricsTool folder.
#
#   .\tools\Run-Metrics.ps1                    # default game project
#   .\tools\Run-Metrics.ps1 -Label "Week 3"    # tag the row in history.csv

param(
    [string]$Project = (Join-Path $PSScriptRoot "..\Sprint-1-Game\Sprint-1-Game.csproj"),
    [string]$OutDir  = (Join-Path $PSScriptRoot "..\metrics"),
    [string]$Label   = ""
)

$ErrorActionPreference = "Stop"
$Project  = (Resolve-Path $Project).Path
$toolProj = Join-Path $PSScriptRoot "CodeMetricsTool\CodeMetricsTool.csproj"

# Restore the game's packages (MonoGame) so Roslyn can resolve every reference.
dotnet restore $Project
if ($LASTEXITCODE -ne 0) { throw "dotnet restore failed for $Project" }

dotnet run --project $toolProj -c Release -- $Project $OutDir $Label
if ($LASTEXITCODE -ne 0) { throw "CodeMetricsTool failed (exit code $LASTEXITCODE)" }