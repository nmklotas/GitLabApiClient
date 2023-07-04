using System;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.AuditEvents.Responses;

public class AuditEvent
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("author_id")]
    public int AuthorId { get; set; }

    [JsonProperty("entity_type")]
    public string EntityType { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
}
