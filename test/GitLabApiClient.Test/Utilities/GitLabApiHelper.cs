using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;

namespace GitLabApiClient.Test.Utilities
{
    internal static class GitLabApiHelper
    {
        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                "http://localhost:9190/api/v4/", new RequestsJsonSerializer(), "ElijahBaley");

            return facade;
        }

        public static string TestProjectTextId { get; set; } = "1";

        public static string TestGroupName { get; set; } = "txxxestgrouxxxp";

        public static string TestProjectName { get; set; } = "txxxestprojecxxxt";

        public static int TestProjectId { get; set; } = 1;
    }
}
