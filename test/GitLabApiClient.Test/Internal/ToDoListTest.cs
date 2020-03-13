using System;
using FluentAssertions;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.ToDoList.Responses;
using Newtonsoft.Json;
using Xunit;

namespace GitLabApiClient.Test.Internal
{
    public class ToDoListTest
    {
        private class MockToDo : IToDo
        {
            public int? Id { get; set; } = 1;
            public Project Project { get; set; } = new Project();
            public Assignee Author { get; set; } = new Assignee();
            public ToDoActionType? ActionType { get; set; } = null;
            public ToDoTargetType? TargetType { get; set; } = null;
            public string TargetUrl { get; set; } = "";
            public string Body { get; set; } = "";
            public ToDoState? State { get; set; } = null;
            public string CreatedAt { get; set; } = "";
        }

        [Fact]
        public void NullToDoTargetTypeThrows()
        {
            var mockToDo = new MockToDo();
            string serializedToDo = JsonConvert.SerializeObject(mockToDo);

            Action action = () => JsonConvert.DeserializeObject<IToDo>(serializedToDo);

            action.Should().Throw<ApplicationException>();
        }

        [Fact]
        public void UnknownToDoTargetTypeThrows()
        {
            var mockToDo = new MockToDo();
            string serializedToDo = JsonConvert.SerializeObject(mockToDo);

            serializedToDo = serializedToDo.Replace("\"TargetType\":null", "\"TargetType\":\"unknownType\"");

            Action action = () => JsonConvert.DeserializeObject<IToDo>(serializedToDo);

            action.Should().Throw<ApplicationException>();
        }

        [Fact]
        public void MissingToDoTargetTypeThrows()
        {
            var mockToDo = new MockToDo();
            string serializedToDo = JsonConvert.SerializeObject(mockToDo);

            serializedToDo = serializedToDo.Replace("\"TargetType\":null,", "");

            Action action = () => JsonConvert.DeserializeObject<IToDo>(serializedToDo);

            action.Should().Throw<ApplicationException>();
        }
    }
}
