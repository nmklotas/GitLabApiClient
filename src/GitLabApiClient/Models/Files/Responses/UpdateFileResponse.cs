using Newtonsoft.Json;

namespace GitLabApiClient.Models.Files.Responses
{
    public class UpdateFileResponse
    {
        /// <summary>
        /// URL encoded full path to new file. Ex. lib%2Fclass%2Erb
        /// </summary>
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        /// <summary>
        /// Name of the branch
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; set; }
    }
}
