using System.Runtime.Serialization;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.ToDoList.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ToDoListQueryBuilder : QueryBuilder<ToDoListQueryOptions>
    {
        protected override void BuildCore(ToDoListQueryOptions options)
        {
            if (options.ActionType != null)
                Add("action", options.ActionType.GetAttribute<EnumMemberAttribute>().Value);

            if (options.AuthorId.HasValue)
                Add("author_id", options.AuthorId.Value);

            if (options.ProjectId.HasValue)
                Add("project_id", options.ProjectId.Value);

            if (options.GroupId.HasValue)
                Add("group_id", options.GroupId.Value);

            if (options.State != null)
                Add("state", options.State.GetAttribute<EnumMemberAttribute>().Value);

            if (options.Type != null)
                Add("type", options.Type.GetAttribute<EnumMemberAttribute>().Value);
        }
    }
}
