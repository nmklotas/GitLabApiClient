using System;
using GitLabApiClient.Models.Groups.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;

namespace GitLabApiClient.Internal.Utilities
{
    internal static class Extensions
    {
        public static bool IsNullOrEmpty(this string value) =>
            string.IsNullOrEmpty(value);

        public static bool IsNotNullOrEmpty(this string value) =>
            !value.IsNullOrEmpty();

        public static string ToLowerCaseString(this object obj) =>
            obj.ToString().ToLowerInvariant();

        /// <summary>
        /// URL encodes a string unless already URL encoded
        /// </summary>
        /// <param name="value">URL path</param>
        /// <returns>Encoded URL path</returns>
        public static string UrlEncode(this string value) =>
            value.Contains("%") ? value : System.Web.HttpUtility.UrlEncode(value);

        public static string ProjectBaseUrl(this object projectId) =>
            $"projects/{projectId.ProjectIdOrPath()}";

        public static string GroupBaseUrl(this object groupId) =>
            $"groups/{groupId.GroupIdOrPath()}";

        public static string ProjectIdOrPath(this object obj)
        {
            switch (obj)
            {
                case null:
                    throw new ArgumentException("ID or Path cannot be null");
                case int id:
                    return id.ToString();
                case string path:
                    return path.UrlEncode();
                case Project project:
                    {
                        int id = project.Id;
                        if (id > 0)
                        {
                            return id.ToString();
                        }

                        string path = project.PathWithNamespace?.Trim();
                        if (!string.IsNullOrEmpty(path))
                        {
                            return path.UrlEncode();
                        }

                        break;
                    }
            }
            throw new ArgumentException($"Cannot determine project ID or Path from provided {obj.GetType().Name} instance. " +
                                        "Must be int, string, Project instance");
        }

        public static string GroupIdOrPath(this object obj)
        {
            switch (obj)
            {
                case null:
                    throw new ArgumentException("ID or Path cannot be null");
                case int id:
                    return id.ToString();
                case string path:
                    return path.UrlEncode();
                case Group group:
                    {
                        int id = group.Id;
                        if (id > 0)
                        {
                            return id.ToString();
                        }

                        string path = group.FullPath?.Trim();
                        if (!string.IsNullOrEmpty(path))
                        {
                            return path.UrlEncode();
                        }

                        break;
                    }
            }
            throw new ArgumentException($"Cannot determine group ID or Path from provided {obj.GetType().Name} instance. " +
                                        "Must be int, string, Group instance");
        }

        public static string UserIdOrPath(this object obj)
        {
            switch (obj)
            {
                case null:
                    throw new ArgumentException("ID or Path cannot be null");
                case int id:
                    return id.ToString();
                case string path:
                    return path.UrlEncode();
                case User user:
                    {
                        int id = user.Id;
                        if (id > 0)
                        {
                            return id.ToString();
                        }

                        string path = user.Username?.Trim();
                        if (!string.IsNullOrEmpty(path))
                        {
                            return path.UrlEncode();
                        }

                        break;
                    }
            }
            throw new ArgumentException($"Cannot determine user ID or Path from provided {obj.GetType().Name} instance. " +
                                        "Must be int, string, User instance");
        }

    }
}
