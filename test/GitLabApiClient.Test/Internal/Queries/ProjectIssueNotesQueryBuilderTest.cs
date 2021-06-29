using FluentAssertions;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Notes.Requests;
using Xunit;

namespace GitLabApiClient.Test.Internal.Queries
{
    public class ProjectIssueNotesQueryBuilderTest
    {
        [Fact]
        public void NonDefaultQueryBuilt()
        {
            var sut = new NotesQueryBuilder();

            string query = sut.Build(
                "https://gitlab.com/api/v4/project/1/issues/1/notes",
                new NotesQueryOptions()
                {
                    SortOrder = SortOrder.Ascending,
                    Order = NoteOrder.UpdatedAt
                });

            query.Should().Be("https://gitlab.com/api/v4/project/1/issues/1/notes?" +
                "sort=asc&" +
                "order_by=updated_at");
        }
    }
}
