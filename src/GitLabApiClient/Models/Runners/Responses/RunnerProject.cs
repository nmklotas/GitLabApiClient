using Newtonsoft.Json;

namespace GitLabApiClient.Models.Runners.Responses
{
    public sealed class RunnerProject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_with_namespace")]
        public string NameWithNamespace { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("path_with_namespace")]
        public string PathWithNamespace { get; set; }
    }

}
