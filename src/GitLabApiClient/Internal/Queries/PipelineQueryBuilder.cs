using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Pipelines;
using GitLabApiClient.Models.Pipelines.Requests;

namespace GitLabApiClient.Internal.Queries
{
    internal sealed class PipelineQueryBuilder : QueryBuilder<PipelineQueryOptions>
    {
        #region Overrides of QueryBuilder<PipelineQueryOptions>

        /// <inheritdoc />
        protected override void BuildCore(Query query, PipelineQueryOptions options)
        {
            if (!options.Ref.IsNullOrEmpty())
            {
                query.Add("ref", options.Ref);
            }

            if (options.YamlErrors.HasValue)
            {
                query.Add("yaml_errors", options.YamlErrors.Value);
            }

            if (!options.Sha.IsNullOrEmpty())
            {
                query.Add("sha", options.Sha);
            }

            if (options.Status != PipelineStatus.All)
            {
                query.Add("status", options.Status.ToLowerCaseString());
            }

            if (options.Scope != PipelineScope.All)
            {
                query.Add("scope", options.Scope.ToLowerCaseString());
            }

            if (options.Order != PipelineOrder.Id)
            {
                query.Add("order_by", AsString(options.Order));
            }

            if (options.SortOrder != SortOrder.Descending)
            {
                query.Add("sort", "asc");
            }

            if (options.UpdatedAfter.HasValue)
            {
                query.Add("updated_after", options.UpdatedAfter.Value);
            }

            if (options.UpdatedBefore.HasValue)
            {
                query.Add("updated_before", options.UpdatedBefore.Value);
            }

            if (!options.TriggeredBy.IsNullOrEmpty())
            {
                query.Add("username", options.TriggeredBy);
            }
        }

        #endregion

        private string AsString(PipelineOrder order)
        {
            switch (order)
            {
                case PipelineOrder.UserId:
                    return "user_id";
                case PipelineOrder.Id:
                case PipelineOrder.Ref:
                case PipelineOrder.Status:
                    return order.ToLowerCaseString();
                default:
                    throw new ArgumentException("Unknown order type", nameof(order));
            }
        }
    }
}
