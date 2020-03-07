using System;
using System.Linq;

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
            value.Contains("%") ? value : System.Uri.EscapeDataString(value);

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var enumType = value.GetType();
            string name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<T>().SingleOrDefault();
        }
    }
}
