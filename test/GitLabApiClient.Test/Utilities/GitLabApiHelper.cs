using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Http.Serialization;

namespace GitLabApiClient.Test.Utilities
{
    internal static class GitLabApiHelper
    {
        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                "https://gitlab.com/api/v4/", new RequestsJsonSerializer(), "yYZSLFnrKyFsG4uD1Wa6");

            return facade;
        }

        public static string TestProjectTextId { get; set; } = "4006344";

        public static int TestProjectId { get; set; } = 4006344;
    }
}
