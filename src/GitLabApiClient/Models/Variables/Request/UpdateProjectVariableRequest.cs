using Newtonsoft.Json;

namespace GitLabApiClient.Models.Variables.Request
{
    public class UpdateProjectVariableRequest
    {
        /// <summary>
        /// The type of a variable.
        /// Available types are: env_var (default) and file
        /// </summary>
        [JsonProperty("variable_type")]
        public string VariableType { get; set; }

        /// <summary>
        /// The key of a variable
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// The value of a variable
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Whether the variable is protected
        /// </summary>
        [JsonProperty("protected")]
        public bool? Protected { get; set; }

        /// <summary>
        /// Whether the variable is masked
        /// </summary>
        [JsonProperty("masked")]
        public bool? Masked { get; set; }

        /// <summary>
        /// The environment_scope of the variable
        /// </summary>
        [JsonProperty("environment_scope")]
        public string EnvironmentScope { get; set; }
    }
}
