using System;
using Xunit;
 
namespace GitLabApiClient.Test.Utilities
{
    [CollectionDefinition("GitLabContainerFixture")]
    public class GitLabContainterFixtureCollection : ICollectionFixture<GitLabContainerFixture>
    {
    }
}