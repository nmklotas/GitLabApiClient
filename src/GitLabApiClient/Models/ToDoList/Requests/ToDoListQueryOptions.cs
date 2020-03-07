using GitLabApiClient.Models.ToDoList.Responses;

namespace GitLabApiClient.Models.ToDoList.Requests
{
    /// <summary>
    /// Options for ToDoList listing
    /// </summary>
    public sealed class ToDoListQueryOptions
    {
        internal ToDoListQueryOptions() { }

        /// <summary>
        /// The action to be filtered. Can be assigned, mentioned, build_failed, 
        /// marked, approval_required, unmergeable or directly_addressed.
        /// </summary>
        public ToDoActionType? ActionType { get; set; } = null;

        /// <summary>
        /// The ID of an author
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// The ID of a project
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// The ID of a group
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// The state of the todo. Can be either pending or done
        /// </summary>
        public ToDoState? State { get; set; }

        /// <summary>
        /// The type of a todo. Can be either Issue or MergeRequest
        /// </summary>
        public ToDoTargetType? Type { get; set; }
    }
}
