$dockerOSType = docker info --format "{{.OSType}}"
if ($dockerOSType -ieq 'linux') {
  & dotnet test --verbosity normal --settings coverletArgs.runsettings --no-build --no-restore
} else {
  & dotnet test --filter Category!=LinuxIntegration --verbosity normal --settings coverletArgs.runsettings --no-build --no-restore
}

$exitWithError = $LastExitCode -ne 0

$openCoverFile = Get-ChildItem -Path "test/*/coverage.opencover.xml" -Recurse | Sort-Object LastWriteTime | Select-Object -last 1
if (Test-Path "$openCoverFile") {
  Write-Host "Uploading coverage file"
  & codecov -f "$openCoverFile" -t bda1c835-c4a2-4a1a-8d38-999b9a8ea80b
}

if ($exitWithError) {
  exit 1
}
