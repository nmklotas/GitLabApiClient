using GitLabApiClient.Models.AuditEvents.Requests;

namespace GitLabApiClient.Internal.Queries;

internal class AuditEventsQueryBuilder : QueryBuilder<AuditEventQueryOptions>
{
    protected override void BuildCore(Query query, AuditEventQueryOptions options)
    {
        if (options.CreatedAfter.HasValue)
            query.Add("created_after", options.CreatedAfter.Value);

        if (options.CreatedBefore.HasValue)
            query.Add("created_before", options.CreatedBefore.Value);
    }
}
