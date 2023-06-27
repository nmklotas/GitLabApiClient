using System;

namespace GitLabApiClient.Models;

public class RateLimitPagedInfo
{
    public int NextPage { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PrevPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }

    public int RateLimitObserved { get; set; }
    public int RateLimitRemaining { get; set; }
    public DateTime RateLimitResetTime { get; set; }
    public int RateLimitLimit { get; set; }
}
