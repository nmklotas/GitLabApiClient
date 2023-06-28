namespace GitLabApiClient.Models;

public class PagingInfo
{
    public int? NextPage { get; set; }
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public int? PrevPage { get; set; }
    public int? Total { get; set; }
    public int? TotalPages { get; set; }
}
