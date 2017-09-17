# GitLabApiClient
[![Build status](https://ci.appveyor.com/api/projects/status/xsauc24bu17311dr?svg=true)](https://ci.appveyor.com/project/nmklotas/gitlabapiclient)[![codecov](https://codecov.io/gh/nmklotas/GitLabApiClient/branch/master/graph/badge.svg)](https://codecov.io/gh/nmklotas/GitLabApiClient)
 [![NuGet](https://img.shields.io/nuget/v/GitLabApiClient.svg)](https://nuget.org/packages/GitLabApiClient)  
GitLabApiClient is a .NET rest client for GitLab API v4 (https://docs.gitlab.com/ce/api/README.html).

## Main features
1. Fully async.
2. Thread safe.
3. Multi core paging.
4. Simple and easy to use.

## Quick start
```csharp

1. Authenticate:

// if you have auth token:
var client =  new GitLabClient("https://gitlab.example.com", "your_private_token");

// if you want to use username & password:
var client =  new GitLabClient("https://gitlab.example.com");
client.LoginAsync("username", "password");

// create a new issue.
await client.Issues.CreateAsync(new CreateIssueRequest("projectId", "issue title"));  

// list issues for a project  with specified assignee and labels.
await client.Issues.GetAsync("projectId", o => o.AssigneeId = 100 && o.Labels == new[] { "test-label" });

// create a new merge request featureBranch -> master.
await client.MergeRequests.CreateAsync(new CreateMergeRequest("projectId", "featureBranch", "master", "Merge request title")
{
    Labels = new[] { "bugfix" },
    Description = "Implement feature"
}); 
```

## Currently supported GitLab APIs:
Issues API:
Create issue.
Update issue.
Query issues.

Merges API:
Create merge request.
Update merge request.
Accept merge request.
Delete merge request.
Query merge requests.

Projects API:
Create project.
Update project.
Delete project.
Get project users.
Query projects.

Users API:
Create user.
Update user.
Delete user.
Get current user session.
Query users.
