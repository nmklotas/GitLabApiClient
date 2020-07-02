using System;
using FluentAssertions;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Projects.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal
{
    /// <summary>
    /// In the GitLab API you can provide the project id as an number (the internal project id) or
    /// path consisting of group path and project path, the path should be URL encoded. GitLabApiClient will handle URL encoding
    ///
    /// Project URL allows
    /// - Alphanumeric characters
    /// - Underscores
    /// - Dashes and dots (it cannot start with dashes or end in a dot)
    /// </summary>
    public class ProjectIdConversionTest
    {
        [Fact]
        public void ProjectId_String_Conversion()
        {
            string expected = "projects/group%2Fmy_awe-some.project";
            ProjectId sut = "group/my_awe-some.project";
            string result = $"projects/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void ProjectId_Int_Conversion()
        {
            string expected = "projects/5";
            ProjectId sut = 5;
            string result = $"projects/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void ProjectId_Project_Path_Conversion()
        {
            string expected = "projects/group%2Fmy_awe-some.project";
            ProjectId sut = new Project { PathWithNamespace = "group/my_awe-some.project" };
            string result = $"projects/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void ProjectId_Project_Id_Conversion()
        {
            string expected = "projects/5";
            ProjectId sut = new Project { Id = 5 };
            string result = $"projects/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void ProjectId_Project_Argument_Exception()
        {
            void TestCode()
            {
                ProjectId projectId = new Project();
            }

            Action act = TestCode;
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot determine project path or id from provided Project instance.");
        }
    }
}
