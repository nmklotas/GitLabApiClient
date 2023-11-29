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
            NextPage = headers.GetFirstHeaderIntValueOrNull("X-Next-Page"),
            Page = headers.GetFirstHeaderIntValueOrNull("X-Page"),
            PerPage = headers.GetFirstHeaderIntValueOrNull("X-Per-Page"),
            PrevPage = headers.GetFirstHeaderIntValueOrNull("X-Prev-Page"),
            Total = headers.GetFirstHeaderIntValueOrNull("X-Total"),
            TotalPages = headers.GetFirstHeaderIntValueOrNull("X-Total-Pages")
        };
}
