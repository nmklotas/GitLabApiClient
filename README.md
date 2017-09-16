# GitLabApiClient
[![Build status](https://ci.appveyor.com/api/projects/status/xsauc24bu17311dr?svg=true)](https://ci.appveyor.com/project/nmklotas/gitlabapiclient)
[![codecov](https://codecov.io/gh/nmklotas/GitLabApiClient/branch/master/graph/badge.svg)](https://codecov.io/gh/nmklotas/GitLabApiClient)  
GitLabApiClient is a rest client for GitLab API v4 (https://docs.gitlab.com/ce/api/README.html).

Main features:
1. Thread safe.
2. Fully async.
3. Multi core paging.
4. Simple and easy to use.

Usage examples:

```csharp

// create client using authorization token:
var client =  new GitLabClient("https://gitlab.example.com", "your_private_token");

// create new issue  
await client.Issues.CreateAsync(new CreateIssueRequest("projectId", "issue title");  

// list issues for a project  
await client.Issues.GetAsync("projectId", o => o.AssigneeId = 100 && o.Labels == new[] { "test-label" });
```

