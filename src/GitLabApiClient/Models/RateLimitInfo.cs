using System;

namespace GitLabApiClient.Models;

public class RateLimitInfo
{
    public int? RateLimitObserved { get; set; }
    public int? RateLimitRemaining { get; set; }
    public DateTime? RateLimitResetTime { get; set; }
    public int? RateLimitLimit { get; set; }
}
