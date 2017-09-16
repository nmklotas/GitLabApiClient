nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 2.4.5.0 ReportGenerator
nuget install -Verbosity quiet -OutputDirectory packages -Version 1.0.3 CodeCov

$coverageFolder = "$PSScriptRoot\coverage"
Remove-Item $coverageFolder -force -recurse -ErrorAction SilentlyContinue | Out-Null
New-Item $coverageFolder -type directory | Out-Null

$openCover="$PSScriptRoot\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe"
$reportGenerator="$PSScriptRoot\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe"
$codeCov = "$PSScriptRoot\packages\CodeCov.1.0.3\tools\codecov.exe"

Write-Host "Calculating coverage with OpenCover."
& $openCover `
  -target:"c:\Program Files\dotnet\dotnet.exe" `
  -targetargs:"test test\GitLabApiClient.Test\GitLabApiClient.Test.csproj" `
  -mergeoutput `
  -hideskipped:File `
  -output:coverage/coverage.xml `
  -oldStyle `
  -filter:"+[GitLabApiClient*]* -[GitLabApiClient.Test]*" `
  -searchdirs:$test/bin/Debug/netcoreapp2.0 `
  -returntargetcode `
  -register:user | Write-Host
  
if ($LastExitCode -ne 0)
{
	exit 1
}

Write-Host "Generating HTML report"
& $reportGenerator `
  -reports:coverage/coverage.xml `
  -targetdir:coverage `
  -verbosity:Error | Write-Host
  
Write-Host "Uploading coverage file"
& $codecov -f "coverage/coverage.xml" -t bda1c835-c4a2-4a1a-8d38-999b9a8ea80b
