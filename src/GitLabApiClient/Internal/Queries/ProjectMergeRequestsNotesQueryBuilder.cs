using System;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Notes.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class ProjectMergeRequestsNotesQueryBuilder : QueryBuilder<MergeRequestNotesQueryOptions>
    {
        protected override void BuildCore(MergeRequestNotesQueryOptions options)
        {
            if (options.SortOrder != SortOrder.Descending)
                Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (options.Order != NoteOrder.CreatedAt)
                Add("order_by", GetNoteOrderQueryValue(options.Order));
        }

        private static string GetNoteOrderQueryValue(NoteOrder order)
        {
            switch (order)
            {
                case NoteOrder.CreatedAt:
                    return "created_at";
                case NoteOrder.UpdatedAt:
                    return "updated_at";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}
