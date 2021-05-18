namespace GitLabApiClient.Models.Commits.Requests
{
    public sealed class CommitStatusesQueryOptions
    {
        public string Ref { get; set; }

        public string Stage { get; set; }

        public string Name { get; set; }

        public bool? All { get; set; }

        internal CommitStatusesQueryOptions()
        {
        }
    }
}
