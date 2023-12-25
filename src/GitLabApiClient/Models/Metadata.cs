using Newtonsoft.Json;

namespace GitLabApiClient.Models;

public class Metadata
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("enterprise")]
    public bool Enterprise { get; set; }
}
