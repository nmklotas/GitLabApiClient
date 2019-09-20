namespace GitLabApiClient.Models.Groups.Requests
{
	/// <summary>
	/// Options for Groups listing
	/// </summary>
	public class ProjectsGroupQueryOptions
    {
        internal ProjectsGroupQueryOptions()
        {
        }

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

        /// <summary>
        /// Limit by projects with issues feature enabled. Default is <code>false</code>
        /// </summary>
        public bool WithIssuesEnabled { get; set; }

        /// <summary>
        /// Limit by projects with merge requests feature enabled. Default is <code>false</code>
        /// </summary>
        public bool WithMergeRequestsEnabled { get; set; }

        /// <summary>
        /// Include projects shared to this group. Default is <code>true</code>
        /// </summary>
        public bool WithShared { get; set; } = true;

        /// <summary>
        /// Include projects in subgroups of this group. Default is <code>false</code>
        /// </summary>
        public bool IncludeSubgroups { get; set; }

        /// <summary>
        /// Limit to projects where current user has at least this <a href="https://docs.gitlab.com/ee/api/members.html">access level</a>
        /// </summary>
        public AccessLevel? MinAccessLevel { get; set; }

        /// <summary>
        /// Include <a href="https://docs.gitlab.com/ee/api/custom_attributes.html">custom attributes</a> in response (admins only)
        /// </summary>
        public bool WithCustomAttributes { get; set; }

        /// <summary>
        /// Return only projects that have security reports artifacts present in any of their builds. This means “projects with security reports enabled”. Default is false
        /// Only Available for GitLab Ultimate or GitLab.com Gold
        /// </summary>
        public bool WithSecurityReports { get; set; }
    }
}
