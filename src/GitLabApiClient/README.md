# Nuget instructions

This package is managed with Nuget, in order to update it, follow the next steps.

* when the code has changed, the Nuget version must increase, there is no "override" mechanism for Nuget versions.
  * go to [GitLabApiClient.csproj]
  * increase the value of `PropertyGroup:Version`
* pack the new code and push it to Nuget using the following lines:
  * cd to the repository directory
  * `dotnet pack --configuration Release`
  * `dotnet nuget push src/GitLabApiClient/bin/Release/Apiiro.GitLabApiClient.0.1.0.nupkg --source "github" --skip-duplicate --no-symbols true`


[GitLabApiClient.csproj]: https://github.com/apiiro/GitLabApiClient/blob/master/src/GitLabApiClient/GitLabApiClient.csproj
