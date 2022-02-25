using GitLabApiClient.Models.Environments.Responses;

namespace GitLabApiClient.Models.Environments.Requests
{
    public sealed class EnvironmentsQueryOptions
    {
        public string Name { get; set; }

        public string Search { get; set; }

        public EnvironmentState? States { get; set; }

        internal EnvironmentsQueryOptions()
        {
        }
    }
}
