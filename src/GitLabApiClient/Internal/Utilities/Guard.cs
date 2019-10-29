using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GitLabApiClient.Internal.Utilities
{
    internal static class Guard
    {
        public static void PathExists(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg) || !(File.Exists(arg) || Directory.Exists(arg)))
                throw new ArgumentException($"ArgumentException: Path not valid {arg}. Parameter name {argName}");
        }

        public static void NotEmpty(string arg, string argName, string message = null)
        {
            if (!string.IsNullOrEmpty(arg))
                return;

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException($"ArgumentException: {argName} string not valid.");

            throw new ArgumentException($"{message}");
        }

        public static void NotNullOrDefault<T>(T arg, string argName)
        {
            if (Equals(arg, default(T)))
                throw new ArgumentException($"ArgumentException: {argName} string not valid.");
        }

        public static void NotEmpty<T>(IEnumerable<T> arg, string argName)
        {
            if (arg == null || !arg.Any())
                throw new ArgumentException($"ArgumentException: sequence {argName} is empty or null");
        }

        public static void NotNull<T>(T arg, string argName)
            where T : class
        {
            if (arg == null)
                throw new ArgumentException($"ArgumentException: {argName} is null");
        }

        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException($"condition not satisfied: {message}");
        }
    }
}
