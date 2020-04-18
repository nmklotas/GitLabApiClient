using System;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Internal.Utilities;

namespace GitLabApiClient.Models.Pipelines.Requests
{
    internal sealed class PipelineQueryBuilder : QueryBuilder<PipelineQueryOptions>
    {
        #region Overrides of QueryBuilder<PipelineQueryOptions>

        /// <inheritdoc />
        protected override void BuildCore(PipelineQueryOptions options)
        {
            if (!options.Ref.IsNullOrEmpty())
            {
                Add("ref", options.Ref);
            }

            if (options.YamlErrors.HasValue)
            {
                Add("yaml_errors", options.YamlErrors.Value);
            }

            if (!options.Sha.IsNullOrEmpty())
            {
                Add("sha", options.Sha);
            }

            if (options.Status != PipelineStatus.All)
            {
                Add("status", options.Status.ToLowerCaseString());
            }

            if (options.Scope != PipelineScope.All)
            {
                Add("scope", options.Scope.ToLowerCaseString());
            }

            if (options.Order != PipelineOrder.Id)
            {
                Add("order_by", AsString(options.Order));
            }

            if (options.SortOrder != SortOrder.Descending)
            {
                Add("sort", "asc");
            }

            if (options.UpdatedAfter.HasValue)
            {
                Add("updated_after", options.UpdatedAfter.Value);
            }

            if (options.UpdatedBefore.HasValue)
            {
                Add("updated_before", options.UpdatedBefore.Value);
            }

            if (!options.TriggeredBy.IsNullOrEmpty())
            {
                Add("username", options.TriggeredBy);
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
