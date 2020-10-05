# GitLabApiClient

[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/nmklotas/GitLabApiClient/DotNet/master?logo=github)](https://github.com/nmklotas/GitLabApiClient/actions?query=workflow%3A%22DotNet%22+branch%3Amaster)
[![NuGet](https://img.shields.io/nuget/v/GitLabApiClient.svg)](https://nuget.org/packages/GitLabApiClient)

GitLabApiClient is a .NET rest client for [GitLab API v4](https://docs.gitlab.com/ce/api/README.html).

## Main features

- Targets .NET Standard 2.0
- Fully async
- Thread safe.
- Multi core paging.
- Simple and natural to use.
- Handles URL encoding for you

## Quick start

### Authenticate

```csharp
// if you have auth token:
var client =  new GitLabClient("https://gitlab.example.com", "your_private_token");
```

```csharp
// if you want to use username & password:
var client =  new GitLabClient("https://gitlab.example.com");
await client.LoginAsync("username", "password");
```

### Use it

```csharp
// create a new issue.
await client.Issues.CreateAsync("group/project", new CreateIssueRequest("issue title"));

// list issues for a project  with specified assignee and labels.
await client.Issues.GetAsync("group/project", o => o.AssigneeId = 100 && o.Labels == new[] { "test-label" });

// create a new merge request featureBranch -> master.
await client.MergeRequests.CreateAsync("group/project", new CreateMergeRequest("featureBranch", "master", "Merge request title")
{
    Labels = new[] { "bugfix" },
    Description = "Implement feature"
});

// get a list of projects and find each project's README.
var projects = await Client.Projects.GetAsync();
foreach (var project in projects)
{
    var file = await Client.Files.GetAsync(project, filePath: "README.md", reference: project.DefaultBranch);
    var readme = file.ContentDecoded;
    // mad science goes here
}
```
