namespace GitLabApiClient.Models.Notes.Requests
{
    /// <summary>
    /// Options for note (comment) listing
    /// </summary>
    public sealed class IssueNotesQueryOptions
    {
        internal IssueNotesQueryOptions() { }

        /// <summary>
        /// Return issue notes sorted in asc or desc order. Default is desc
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.Descending;

        /// <summary>
        /// Return issue notes ordered by created_at or updated_at fields. Default is created_at
        /// </summary>
        public NoteOrder Order { get; set; } = NoteOrder.CreatedAt;
    }
}
