namespace GitLabApiClient.Models.Branches.Requests
{
    public sealed class BranchQueryOptions
    {
        public string Search { get; set; }

        public int? Page { get; set; }

        public int? PerPage { get; set; }

        internal BranchQueryOptions()
        {
        }
    }
}
