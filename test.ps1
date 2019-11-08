& dotnet test --filter Category!=LinuxIntegration --settings coverletArgs.runsettings --no-build --no-restore

if ($LastExitCode -ne 0)
{
	exit 1
}

Write-Host "Uploading coverage file"
$openCoverFile = (Get-ChildItem -Path "test/*/coverage.opencover.xml" -Recurse | Sort-Object LastWriteTime | Select-Object -last 1).FullName
& codecov -f "$openCoverFile" -t bda1c835-c4a2-4a1a-8d38-999b9a8ea80b
