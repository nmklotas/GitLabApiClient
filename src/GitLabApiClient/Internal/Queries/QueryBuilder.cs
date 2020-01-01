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
        private readonly NameValueCollection _nameValues = new NameValueCollection();

        public string Build(string baseUrl, T options)
        {
            _nameValues.Clear();
            BuildCore(options);
            return baseUrl + ToQueryString(_nameValues);
        }

        protected abstract void BuildCore(T options);

        protected void Add(string name, string value)
            => _nameValues.Add(name, value);

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

        private static string ToQueryString(NameValueCollection nvc)
        {
            var array =
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select $"{key.UrlEncode()}={value.UrlEncode()}";

            return $"?{string.Join("&", array)}";
        }
    }
}
