using System.Runtime.Serialization;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.ToDoList.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class ToDoListQueryBuilder : QueryBuilder<ToDoListQueryOptions>
    {
        protected override void BuildCore(Query query, ToDoListQueryOptions options)
        {
            if (options.ActionType != null)
                query.Add("action", options.ActionType.GetAttribute<EnumMemberAttribute>().Value);

            if (options.AuthorId.HasValue)
                query.Add("author_id", options.AuthorId.Value);

            if (options.ProjectId.HasValue)
                query.Add("project_id", options.ProjectId.Value);

            if (options.GroupId.HasValue)
                query.Add("group_id", options.GroupId.Value);

            if (options.State != null)
                query.Add("state", options.State.GetAttribute<EnumMemberAttribute>().Value);

            if (options.Type != null)
                query.Add("type", options.Type.GetAttribute<EnumMemberAttribute>().Value);
        }
    }
}
