namespace GitLabApiClient.Models.Branches.Requests
{
    public sealed class BranchQueryOptions
    {
        public string Search { get; set; }

        public PageOptions PageOptions { get; set; }

        internal BranchQueryOptions()
        {
        }
    }
}
