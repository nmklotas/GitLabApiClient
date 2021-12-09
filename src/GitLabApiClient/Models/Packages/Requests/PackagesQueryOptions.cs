using System;
using System.Collections.Generic;
using GitLabApiClient.Models.Packages.Responses;

namespace GitLabApiClient.Models.Packages.Requests
{
    /// <summary>
    /// Options for issues listing
    /// </summary>
    public class PackagesQueryOptions
    {
        internal PackagesQueryOptions() { }


        /// <summary>
        /// Return all packages, or packages with a specific status
        /// Default is Default.
        /// </summary>
        public PackageStatus Status { get; set; }

        /// <summary>
        /// Filter the returned packages by type. One of conan, maven, npm, pypi, composer, nuget, helm, or golang. (Introduced in GitLab 12.9)
        /// Defaults to packages of all types. (Introduced in GitLab 9.5).
        /// </summary>
        public PackageType PackageType { get; set; }

        /// <summary>
        /// Filter the project packages with a fuzzy search by name. (Introduced in GitLab 12.9)
        /// </summary>
        public string PackageName { get; set; }


        /// <summary>
        /// Specifies issues order. Default is Creation time.
        /// </summary>
        public PackagesOrder Order { get; set; }

        /// <summary>
        /// Specifies project sort order. Default is descending.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// When set to true, versionless packages are included in the response. (Introduced in GitLab 13.8)
        /// </summary>
        public bool IncludeVersionless { get; set; } = false;
    }
}
