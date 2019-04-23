# GitLabApiClient
[![Build status](https://ci.appveyor.com/api/projects/status/xsauc24bu17311dr?svg=true)](https://ci.appveyor.com/project/nmklotas/gitlabapiclient)
[![Build Status](https://travis-ci.org/nmklotas/GitLabApiClient.svg?branch=master)](https://travis-ci.org/nmklotas/GitLabApiClient)
 [![NuGet](https://img.shields.io/nuget/v/GitLabApiClient.svg)](https://nuget.org/packages/GitLabApiClient)  
GitLabApiClient is a .NET rest client for GitLab API v4 (https://docs.gitlab.com/ce/api/README.html).

## Main features
1. Targets .NET Standard 2.0.
2. Fully async.
3. Thread safe.
4. Multi core paging.
5. Simple and natural to use.

## Quick start


1. Authenticate:

```csharp
// if you have auth token:
var client =  new GitLabClient("https://gitlab.example.com", "your_private_token");
```

```csharp
// if you want to use username & password:
var client =  new GitLabClient("https://gitlab.example.com");
await client.LoginAsync("username", "password");
```

2. Use it:
```csharp
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
