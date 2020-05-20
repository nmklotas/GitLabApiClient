using System;
using System.Collections.Generic;
using System.Linq;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models;

namespace GitLabApiClient.Internal.Queries
{
    internal abstract class QueryBuilder<T>
    {
        private readonly List<string> _nameValues = new List<string>();

        public string Build(string baseUrl, T options)
        {
            BuildCore(options);
            return baseUrl + ToQueryString(_nameValues);
        }

        protected abstract void BuildCore(T options);

        protected void Add(string name, string value)
            => _nameValues.Add(ToQueryString(name, value));

        protected void Add(string name, bool value)
            => Add(name, value.ToLowerCaseString());

        protected void Add(string name, int value)
            => Add(name, value.ToLowerCaseString());

        protected void Add(string name, DateTime value)
            => Add(name, value.ToString("o"));

        protected void Add(string name, IList<string> values)
        {
            if (!values.Any())
                return;

            Add(name, string.Join(",", values));
        }

        protected void Add(string name, IList<int> values)
        {
            foreach (int val in values)
                Add($"{name}[]", val.ToString());
        }

        protected void Add(IList<int> values)
        {
            foreach (int iid in values)
                Add("iids[]", iid.ToString());
        }

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

        private static string ToQueryString(List<string> dic)
        {
            return $"?{string.Join("&", dic)}";
        }

        private static string ToQueryString(string key, string value) => $"{key.UrlEncode()}={value.UrlEncode()}";
    }
}
