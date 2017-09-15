namespace GitLabApiClient.Internal.Utilities
{
    internal static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string ToLowerCaseString(this object obj)
        {
            return obj.ToString().ToLowerInvariant();
        }
    }
}
