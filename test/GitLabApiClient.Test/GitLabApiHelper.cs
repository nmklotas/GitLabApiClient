using GitLabApiClient.Http;

namespace GitLabApiClient.Test
{
    internal static class GitLabApiHelper
    {
        public static GitLabHttpFacade GetFacade()
        {
            var facade = new GitLabHttpFacade(
                "https://gitlab.com/api/v3", "yYZSLFnrKyFsG4uD1Wa6");

            return facade;
        }

        public static int TestProjectId { get; set; } = 4006344;
    }
}
