using System;
using FluentAssertions;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Users.Responses;
using Xunit;

namespace GitLabApiClient.Test.Internal
{
    /// <summary>
    /// In the GitLab API you can provide the user id as an number (the internal user id)
    /// </summary>
    public class UserIdConversionTest
    {
        [Fact]
        public void UserId_Int_Conversion()
        {
            string expected = "users/5";
            UserId sut = 5;
            string result = $"users/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void UserId_User_Id_Conversion()
        {
            string expected = "users/5";
            UserId sut = new User { Id = 5 };
            string result = $"users/{sut}";
            result.Should().Be(expected);
        }

        [Fact]
        public void UserId_User_Argument_Exception()
        {
            void TestCode()
            {
                UserId userId = new User();
            }

            Action act = TestCode;
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot determine user id from provided User instance.");
        }
    }
}
