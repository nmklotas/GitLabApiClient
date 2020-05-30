using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;

namespace GitLabApiClient.Internal.Queries
{
    internal abstract class QueryBuilder<T>
    {
        protected class Query
        {
            private readonly NameValueCollection _nameValues = new NameValueCollection();

            public void Add(string name, string value)
                => _nameValues.Add(name, value);

            public void Add(string name, bool value)
                => Add(name, value.ToLowerCaseString());

            public void Add(string name, int value)
                => Add(name, value.ToLowerCaseString());

            public void Add(string name, DateTime value)
                => Add(name, value.ToString("o"));

            public void Add(string name, IList<string> values)
            {
                if (!values.Any())
                    return;

                Add(name, string.Join(",", values));
            }

            public void Add(string name, IList<int> values)
            {
                foreach (int val in values)
                    Add($"{name}[]", val.ToString());
            }

            public void Add(IList<int> values)
            {
                foreach (int iid in values)
                    Add("iids[]", iid.ToString());
            }

            public string ToQueryString()
            {
                var array = _nameValues.AllKeys.SelectMany(
                        key => _nameValues.GetValues(key)
                            ?.Select(value => $"{key.UrlEncode()}={value.UrlEncode()}")
                    )
                    .ToArray();
                return array.Any() ? "?" + string.Join("&", array) : "";
            }
        }

        public string Build(string baseUrl, T options)
        {
            var query = new Query();
            BuildCore(query, options);
            return baseUrl + query.ToQueryString();
        }

        protected abstract void BuildCore(Query query, T options);

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
    }
}
