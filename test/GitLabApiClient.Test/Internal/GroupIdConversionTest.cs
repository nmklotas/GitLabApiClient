using System;
using FluentAssertions;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Groups.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal
{
    /// <summary>
    /// In the GitLab API you can provide the group id as an number (the internal group id) or
    /// path consisting of group path, the path should be URL encoded. GitLabApiClient will handle URL encoding
    ///
    /// Group/Subgroup URL allows
    /// - Alphanumeric characters
    /// - Underscores
    /// - Dashes and dots (it cannot start with dashes or end in a dot)
    /// </summary>
    public class GroupIdConversionTest
    {
        [Fact]
        public void GroupId_String_Conversion()
        {
            string expected = "groups/group%2Fmy_awe-some.subgroup%2Fsubgroup";
            GroupId sut = "group/my_awe-some.subgroup/subgroup";
            string result = $"groups/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void GroupId_Int_Conversion()
        {
            string expected = "groups/5";
            GroupId sut = 5;
            string result = $"groups/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void GroupId_Group_Path_Conversion()
        {
            string expected = "groups/group%2Fmy_awe-some.subgroup%2Fsubgroup";
            GroupId sut = new Group { FullPath = "group/my_awe-some.subgroup/subgroup" };
            string result = $"groups/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void GroupId_Group_Id_Conversion()
        {
            string expected = "groups/5";
            GroupId sut = new Group { Id = 5 };
            string result = $"groups/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void GroupId_Group_Argument_Exception()
        {
            void TestCode()
            {
                GroupId groupId = new Group();
            }

            Action act = TestCode;
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot determine group path or id from provided Group instance.");
        }
    }
}
