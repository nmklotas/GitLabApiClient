using System;

namespace GitLabApiClient.Models.AuditEvents.Requests;

public class AuditEventQueryOptions : PagingQueryOptions
{
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
}
