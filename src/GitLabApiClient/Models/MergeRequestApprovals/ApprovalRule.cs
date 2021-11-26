using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.MergeRequestApprovals
{
    public class ApprovalRule
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rule_type")]
        public string RuleType { get; set; }

    }
}
