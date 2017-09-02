using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using GitLabApiClient.Models;
using static System.Web.HttpUtility;

namespace GitLabApiClient
{
    internal abstract class QueryBuilder<T>
    {
        public string Build(string baseUrl, T options)
        {
            var nameValues = new NameValueCollection();
            BuildCore(nameValues, options);
            return baseUrl + ToQueryString(nameValues);
        }

        protected abstract void BuildCore(NameValueCollection nameValues, T options);

        protected static string ToQueryString(IEnumerable<string> values) => string.Join(",", values);

        protected static string GetDateValue(DateTime dateTime) => dateTime.ToString("o");

        protected static string GetSortOrderQueryValue(SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Ascending:
                    return "asc";
                case SortOrder.Descending:
                    return "desc";
                default:
                    throw new NotSupportedException($"Order {order} not supported");
            }
        }

        protected static string GetScopeQueryValue(Scope scope)
        {
            switch (scope)
            {
                case Scope.CreatedByMe:
                    return "created-by-me";
                case Scope.AssignedToMe:
                    return "assigned-to-me";
                case Scope.All:
                    return "all";
                default:
                    throw new NotSupportedException($"Scope {scope} is not supported");
            }
        }

        private static string ToQueryString(NameValueCollection nvc)
        {
            var array =
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format("{0}={1}", UrlEncode(key), UrlEncode(value));

            return "?" + string.Join("&", array);
        }
    }
}
