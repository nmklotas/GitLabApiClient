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
            RateLimitObserved = headers.GetFirstHeaderIntValueOrNull("RateLimit-Observed"),
            RateLimitRemaining = headers.GetFirstHeaderIntValueOrNull("RateLimit-Remaining"),
            RateLimitResetTime = headers.GetFirstHeaderDateTimeValueOrNull("RateLimit-ResetTime"),
            RateLimitLimit = headers.GetFirstHeaderIntValueOrNull("RateLimit-Limit")
        };
}
