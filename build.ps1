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
	if ($env:APPVEYOR_REPO_TAG_NAME) 
	{
		$version = $env:APPVEYOR_REPO_TAG_NAME
		& dotnet pack src\GitLabApiClient\GitLabApiClient.csproj -c Release --version-suffix $version --no-build | Write-Host
	}
	else
	{
		& dotnet pack src\GitLabApiClient\GitLabApiClient.csproj -c Release --no-build | Write-Host
	}
		
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