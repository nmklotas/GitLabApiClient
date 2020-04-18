using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models
{
    public class PageQuery
    {
        public int? Page { get; set; }
        public int? Per_Page { get; set; }
    }
}
