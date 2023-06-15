namespace GitLabApiClient.Models.Branches.Requests
{
    public sealed class BranchQueryOptions
    {
        public string Search { get; set; }

        public string Page { get; set; }

        public string PerPage { get; set; }

        internal BranchQueryOptions()
        {
        }
    }
}
