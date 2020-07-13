namespace GitLabApiClient.Models
{
    /// <summary>
    /// Request options for pagination
    /// </summary>
    public class PaginationOptions
    {
        /// <summary>
        /// Requested page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Maximum number of results returned per page
        /// </summary>
        public int? MaxItemsPerPage { get; set; }
    }
}
