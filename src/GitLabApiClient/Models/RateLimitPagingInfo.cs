using System.Net.Http.Headers;

namespace GitLabApiClient.Models;

public class RateLimitPagingInfo
{
    public RateLimitPagingInfo(PagingInfo pagingInfo, RateLimitInfo rateLimitInfo)
    {
        PagingInfo = pagingInfo;
        RateLimitInfo = rateLimitInfo;
    }

    public PagingInfo PagingInfo { get; init; }

    public RateLimitInfo RateLimitInfo { get; init; }

    public static RateLimitPagingInfo FromHeaders(HttpResponseHeaders headers)
        => new(PagingInfo.FromHeaders(headers), RateLimitInfo.FromHeaders(headers));
}
