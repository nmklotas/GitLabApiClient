namespace GitLabApiClient.Models.Iterations.Requests
{
    /// <summary>
    /// Options for issues listing
    /// </summary>
    public class IterationsQueryOptions
    {
        internal IterationsQueryOptions() { }

        /// <summary>
        /// Return opened, upcoming, current (previously started), closed, or
        /// all iterations. Filtering by started state is deprecated starting
        /// with 14.1, please use current instead.
        /// </summary>
        public IterationState? State { get; set; }

        /// <summary>
        /// Return only iterations with a title matching the provided string.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Include iterations from parent group and its ancestors.
        /// </summary>
        public bool IncludeAncestors { get; set; } = true;

        /// <summary>
        /// Filter for iterations with the given title.
        /// </summary>
        public string Title { get; set; }
    }
}
