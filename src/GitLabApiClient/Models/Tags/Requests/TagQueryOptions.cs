namespace GitLabApiClient.Models.Tags.Requests
{
    public sealed class TagQueryOptions
    {
        public string Search { get; set; }
        public TagOrder OrderBy { get; set; } = TagOrder.Name;
        public TagSort Sort { get; set; } = TagSort.Desc;

        internal TagQueryOptions()
        {
        }
    }
}
