using Newtonsoft.Json;

namespace GitLabApiClient.Models.Projects.Requests
{
    public class CreateRemoteMirrorRequest
    {

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Gets or sets the authentication method.
        /// </summary>
        /// <value>
        /// ssh_public_key or password
        /// </value>
        [JsonProperty("auth_method", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthMethod { get; set; }

        [JsonProperty("only_protected_branches", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyProtectedBranches { get; set; }

        [JsonProperty("keep_divergent_refs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KeepDivergentRefs { get; set; }
    }
}
