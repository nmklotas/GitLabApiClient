using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Models.Issues.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal class EpicsGroupQueryBuilder : QueryBuilder<EpicsGroupQueryOptions>
    {
        protected override void BuildCore(Query query, EpicsGroupQueryOptions options)
        {
            if (options.AuthorId.HasValue)
                query.Add("author_id", options.AuthorId.Value);

            if (options.Labels.Any())
                query.Add("labels", options.Labels);

            if(options.WithLabelsDetails)
                query.Add("with_labels_details", options.WithLabelsDetails);

            if (options.Order != EpicsIssuesOrder.CreatedAt)
                query.Add("order_by", Order.GetIssuesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (!options.Filter.IsNullOrEmpty())
                query.Add("search", options.Filter);

            string stateQueryValue = State.GetStateQueryValue(options.State);
            if (!stateQueryValue.IsNullOrEmpty())
                query.Add("state", stateQueryValue);

            if (options.CreatedBefore.HasValue)
                query.Add("created_before", options.CreatedBefore.Value);

            if (options.CreatedAfter.HasValue)
                query.Add("created_after", options.CreatedAfter.Value);

            if (options.UpdatedBefore.HasValue)
                query.Add("updated_before", options.UpdatedBefore.Value);

            if (options.UpdatedAfter.HasValue)
                query.Add("updated_after", options.UpdatedAfter.Value);

            if (options.IncludeAncestorGroups)
                query.Add("include_ancestor_groups", options.IncludeAncestorGroups);

            if (options.IncludeDescendantGroups)
                query.Add("include_descendant_groups", options.IncludeDescendantGroups);
        }
    }
}
