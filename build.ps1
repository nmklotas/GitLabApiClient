function Build()
{
	& dotnet restore | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}

	& dotnet build --no-restore | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}

	& dotnet build -c Release --no-restore | Write-Host
	if ($LastExitCode -ne 0)
	{
		exit 1
	}
}

function Pack()
{
	& dotnet pack -c Release --no-restore --no-build | Write-Host
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
