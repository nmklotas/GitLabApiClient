namespace GitLabApiClient.Models.Groups.Requests
{
	/// <summary>
	/// Options for Groups listing
	/// </summary>
	public class ProjectsGroupQueryOptions
    {
        internal ProjectsGroupQueryOptions(string id) => Id = id;

        /// <summary>
        /// The ID or URL-encoded path of the group owned by the authenticated user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
		/// Limit by archived status
		/// </summary>
		public bool Archived { get; set; }

		/// <summary>
		/// Limit by visibility public, internal, or private
		/// </summary>
		public GroupsVisibility Visibility { get; set; }

		/// <summary>
		/// Return projects ordered by Id, Name, Path, CreatedAt, UpdatedAt, 
		/// or LastActivityAt fields. Default is CreatedAt.
		/// </summary>
		public GroupsProjectsOrder Order { get; set; }

		/// <summary>
		/// Return projects sorted in asc or desc order. Default is desc
		/// </summary>
		public GroupsSort Sort { get; set; }

		/// <summary>
		/// Return list of authorized projects matching the search criteria
		/// </summary>
		public string Search { get; set; }

		/// <summary>
		/// Return only the ID, URL, name, and path of each project
		/// </summary>
		public bool Simple { get; set; }

		/// <summary>
		/// Limit by projects owned by the current user
		/// </summary>
		public bool Owned { get; set; }

		/// <summary>
		/// Limit by projects starred by the current user
		/// </summary>
		public bool Starred { get; set; }
    }
}
