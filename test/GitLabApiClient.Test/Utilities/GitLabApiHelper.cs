using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;

namespace GitLabApiClient.Test.Utilities
{
    internal static class GitLabApiHelper
    {
        private const string PrivateAuthenticationToken = "ElijahBaley";

        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                "http://localhost:9190/api/v4/", new RequestsJsonSerializer(), PrivateAuthenticationToken);

            return facade;
        }

        public static string TestProjectTextId { get; set; } = "1";

        public static int TestProjectId { get; set; } = 1;

        public static string TestGroupName { get; set; } = "txxxestgrouxxxp";

        public static string TestProjectName { get; set; } = "txxxestprojecxxxt";

        public static string TestUserName { get; set; } = "root";

        public static string TestName { get; set;} = "Administrator";

        public static string TestPassword { get; set; } = "hariseldon";
    }
}
