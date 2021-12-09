using System;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Packages.Requests;
using GitLabApiClient.Models.Packages.Responses;

namespace GitLabApiClient.Internal.Queries
{
    internal class PackagesQueryBuilder : QueryBuilder<PackagesQueryOptions>
    {
        protected override void BuildCore(Query query, PackagesQueryOptions options)
        {
            string stateQueryValue = GetStatusQueryValue(options.Status);
            if (!stateQueryValue.IsNullOrEmpty())
                query.Add("status", stateQueryValue);

            string packageTypeQueryValue = GetPackageTypeQueryValue(options.PackageType);
            if (!packageTypeQueryValue.IsNullOrEmpty())
                query.Add("package_type", packageTypeQueryValue);

            if (!string.IsNullOrEmpty(options.PackageName))
                query.Add("package_name", options.PackageName);

            if (options.Order != PackagesOrder.CreatedAt)
                query.Add("order_by", GetPackagesOrderQueryValue(options.Order));

            if (options.SortOrder != SortOrder.Descending)
                query.Add("sort", GetSortOrderQueryValue(options.SortOrder));

            if (options.IncludeVersionless)
                query.Add("include_versionless", true);
        }

        private static string GetPackageTypeQueryValue(PackageType type) => type == PackageType.All ? "" : type.ToString().ToLower();

        private static string GetStatusQueryValue(PackageStatus status)
        {
            switch (status)
            {
                case PackageStatus.Default:
                    return "";
                case PackageStatus.Hidden:
                    return "hidden";
                case PackageStatus.Processing:
                    return "processing";
                default:
                    throw new NotSupportedException($"Status {status} is not supported");
            }
        }

        private static string GetPackagesOrderQueryValue(PackagesOrder order)
        {
            switch (order)
            {
                case PackagesOrder.CreatedAt:
                    return "created_at";
                case PackagesOrder.Name:
                    return "name";
                case PackagesOrder.Version:
                    return "version";
                case PackagesOrder.Type:
                    return "type";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}
