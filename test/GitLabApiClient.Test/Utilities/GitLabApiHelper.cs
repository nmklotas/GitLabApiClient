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

        public static string TestUserName { get; set; } = "root";

        public static string TestName { get; set; } = "Administrator";

        public static string TestDescription { get; set; } = "This is just a test-description";
    }
}
