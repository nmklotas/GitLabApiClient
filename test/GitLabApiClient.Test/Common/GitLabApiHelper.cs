using GitLabApiClient.Http;

namespace GitLabApiClient.Test.Common
{
    internal static class GitLabApiHelper
    {
        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                "https://gitlab.com/api/v4/", "yYZSLFnrKyFsG4uD1Wa6");

            return facade;
        }

        public static string TestProjectTextId { get; set; } = "4006344";

        public static int TestProjectId { get; set; } = 4006344;
    }
}
