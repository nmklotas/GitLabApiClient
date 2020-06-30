using System;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.MergeRequests.Responses;
using GitLabApiClient.Models.Projects.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace GitLabApiClient.Models.ToDoList.Responses
{

    [JsonConverter(typeof(ToDoItemConverter))]
    public interface IToDo
    {
        int? Id { get; set; }
        Project Project { get; set; }
        Assignee Author { get; set; }
        ToDoActionType? ActionType { get; set; }
        ToDoTargetType? TargetType { get; set; }
        string TargetUrl { get; set; }
        string Body { get; set; }
        ToDoState? State { get; set; }
        string CreatedAt { get; set; }
    }

    public abstract class ToDo : IToDo
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("author")]
        public Assignee Author { get; set; }

        [JsonProperty("action_name")]
        public ToDoActionType? ActionType { get; set; }

        [JsonProperty("target_type")]
        public ToDoTargetType? TargetType { get; set; }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public ToDoState? State { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public sealed class ToDoIssue : ToDo
    {
        [JsonProperty("target")]
        public Issue Target { get; set; }
    }

    public sealed class ToDoMergeRequest : ToDo
    {
        [JsonProperty("target")]
        public MergeRequest Target { get; set; }
    }

    public class ToDoItemConverter : CustomCreationConverter<IToDo>
    {
        public override IToDo Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public IToDo Create(JObject jObject)
        {
            string type = (string)jObject.Property("target_type");
            switch (type)
            {
                case "Issue":
                    return new ToDoIssue();
                case "MergeRequest":
                    return new ToDoMergeRequest();
                default:
                    throw new ApplicationException($"ToDo target not supported.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }
    }
}
