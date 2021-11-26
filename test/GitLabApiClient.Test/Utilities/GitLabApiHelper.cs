using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;

namespace GitLabApiClient.Test.Utilities
{
    internal static class GitLabApiHelper
    {
        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                GitLabContainerFixture.GitlabHost, new RequestsJsonSerializer(), GitLabContainerFixture.Token);

            return facade;
        }

        public static string TestProjectTextId { get; set; } = "1";

        public static int TestProjectId { get; set; } = 1;

        public static string TestGroupTextId { get; set; } = "2";

        public static int TestGroupId { get; set; } = 2;

        public static string TestGroupName { get; set; } = "txxxestgrouxxxp";

        public static string TestProjectName { get; set; } = "txxxestprojecxxxt";

        public static int TestUserId { get; set; } = 1;

        public static string TestUserName { get; set; } = "root";

        public static string TestUserPassword { get; set; } = "password";

        public static string TestName { get; set; } = "Administrator";
        public static string TestUserEmail { get; set; } = "admin@example.com";
        public static int TestExtraUserId { get; set; } = 2;
        public static string TestExtraUserName { get; set; } = "txxxestusexxxr";

        public static string TestExtraUserPassword { get; set; } = "txxxestusexxxr_password";

        public static string TestExtraName { get; set; } = "Txxxest Usexxxr";
        public static string TestExtraUserEmail { get; set; } = "txxxestusexxxr@example.com";

        public static string TestDescription { get; set; } = "This is just a test-description";

        public static string TestRunnerName { get; set; } = "txxxestrunnexxxr";

        public static string TestGroupRunnerName { get; set; } = "txxxestrunnexxxr_group";

        public static string TestProjectRunnerName { get; set; } = "txxxestrunnexxxr_project";
    }
}
