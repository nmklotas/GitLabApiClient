function Build()
{
	& dotnet restore GitLabApiClient.sln | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}
	
	& dotnet build src\GitLabApiClient\GitLabApiClient.csproj | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}

	& dotnet build src\GitLabApiClient\GitLabApiClient.csproj -c Release | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}
}

function Pack()
{
	$packageVersion = "$env:APPVEYOR_REPO_TAG_NAME"
	if (!$packageVersion) {
		return
	}

	& dotnet pack src\GitLabApiClient\GitLabApiClient.csproj -c Release --no-build -p:PackageVersion=$packageVersion | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}
}

function Main()
{
	Build
	Pack
}

Main