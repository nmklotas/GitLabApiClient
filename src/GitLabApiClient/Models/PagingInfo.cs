using System.Net.Http.Headers;
using GitLabApiClient.Internal.Http;

namespace GitLabApiClient.Models;

public class PagingInfo
{
    public int? NextPage { get; init; }
    public int? Page { get; init; }
    public int? PerPage { get; init; }
    public int? PrevPage { get; init; }
    public int? Total { get; init; }
    public int? TotalPages { get; init; }

    public static PagingInfo FromHeaders(HttpResponseHeaders headers)
        => new()
        {
            NextPage = headers.GetFirstHeaderValueOrDefault<int?>("X-Next-Page"),
            Page = headers.GetFirstHeaderValueOrDefault<int?>("X-Page"),
            PerPage = headers.GetFirstHeaderValueOrDefault<int?>("X-Per-Page"),
            PrevPage = headers.GetFirstHeaderValueOrDefault<int?>("X-Prev-Page"),
            Total = headers.GetFirstHeaderValueOrDefault<int?>("X-Total"),
            TotalPages = headers.GetFirstHeaderValueOrDefault<int?>("X-Total-Pages")
        };
}
