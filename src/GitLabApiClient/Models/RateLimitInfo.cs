using System;
using System.Net.Http.Headers;
using GitLabApiClient.Internal.Http;

namespace GitLabApiClient.Models;

public class RateLimitInfo
{
    public int? RateLimitObserved { get; init; }
    public int? RateLimitRemaining { get; init; }
    public DateTime? RateLimitResetTime { get; init; }
    public int? RateLimitLimit { get; init; }

    public static RateLimitInfo FromHeaders(HttpResponseHeaders headers)
        => new()
        {
            RateLimitObserved = headers.GetFirstHeaderValueOrDefault<int?>("RateLimit-Observed"),
            RateLimitRemaining = headers.GetFirstHeaderValueOrDefault<int?>("RateLimit-Remaining"),
            RateLimitResetTime = headers.GetFirstHeaderValueOrDefault<DateTime?>("RateLimit-ResetTime"),
            RateLimitLimit = headers.GetFirstHeaderValueOrDefault<int?>("RateLimit-Limit")
        };
}
