using Newtonsoft.Json;

namespace GitLabApiClient.Models.Notes.Responses
{
    public sealed class Note : ModifiableObject
    {
        [JsonProperty("noteable_id")]
        public int NoteableId { get; set; }

        [JsonProperty("noteable_iid")]
        public int NoteableIid { get; set; }

        [JsonProperty("noteable_type")]
        public string NoteableType { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("system")]
        public bool System { get; set; }

        [JsonProperty("resolvable")]
        public bool Resolvable { get; set; }
    }
}
