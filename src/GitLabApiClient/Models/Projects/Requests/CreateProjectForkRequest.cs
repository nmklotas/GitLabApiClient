using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    public sealed class CreateProjectForkRequest
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectRequest"/> class.
        /// <param name="name">The name of the new project.</param>
        /// </summary>
        public static CreateProjectForkRequest FromName(string name)
        {
            Guard.NotEmpty(name, nameof(name));
            return new CreateProjectForkRequest
            {
                Name = name,
                Path = name.ToLower()
            };
        }

        private CreateProjectForkRequest() { }

        /// <summary>
        /// The description assigned to the resultant project after forking
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// For forked projects, target merge requests to this project. If <see cref="false"/>, the target is the upstream project
        /// </summary>
        [JsonProperty("mr_default_target_self")]
        public bool DefaultTargetSelf { get; set; }

        /// <summary>
        /// The name assigned to the resultant project after forking
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the namespace that the project is forked to
        /// </summary>
        [JsonProperty("namespace_id")]
        public int NamespaceId { get; set; }

        /// <summary>
        /// The path of the namespace that the project is forked to
        /// </summary>
        [JsonProperty("namespace_path")]
        public int NamespacePath { get; set; }

        /// <summary>
        /// The path assigned to the resultant project after forking
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// The <see cref="ProjectVisibilityLevel"/> assigned to the resultant project after forking
        /// </summary>
        [JsonProperty("visibility")]
        public ProjectVisibilityLevel? Visibility { get; set; }

    }
}
