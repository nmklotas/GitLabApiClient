using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using GitLabApiClient.Models.Issues.Requests;
using Xunit;

namespace GitLabApiClient.Test.Inspections
{
    public class RequestsInspectionTest
    {
        [Fact]
        public void AllRequestsDoesNotHaveBoolProperties()
        {
            var requestClasses = GetRequestClasses();

            foreach (var requestClass in requestClasses)
            {
                var boolProperties = requestClass.GetProperties(BindingFlags.Public | BindingFlags.Instance).
                    Where(p => p.PropertyType == typeof(bool)).
                    ToList();

                boolProperties.Should().BeEmpty($"Request class should not have bool property. Use bool?. Class name: {requestClass.Name}.");
            }
        }

        [Fact]
        public void AllRequestsDoesNotHaveEnumProperties()
        {
            var requestClasses = GetRequestClasses();

            foreach (var requestClass in requestClasses)
            {
                var enumProperties = requestClass.GetProperties(BindingFlags.Public | BindingFlags.Instance).
                    Where(p => p.PropertyType.IsEnum).
                    ToList();

                enumProperties.Should().BeEmpty($"Request class should not have enum property Use enum?. Class name: {requestClass.Name}.");
            }
        }

        [Fact]
        public void AllRequestsDoesNotHaveWritableIntProperties()
        {
            var requestClasses = GetRequestClasses();


            foreach (var requestClass in requestClasses)
            {
                var intProperties = requestClass.GetProperties(BindingFlags.Public | BindingFlags.Instance).
                    Where(p => p.PropertyType == typeof(int) || p.PropertyType == typeof(uint) || p.PropertyType == typeof(long)).
                    Where(p => p.SetMethod != null && p.SetMethod.IsPublic).
                    ToList();

                intProperties.Should().BeEmpty($"Request class should not have numeric property Use numeric?. Class name: {requestClass.Name}.");
            }
        }

        private static IEnumerable<Type> GetRequestClasses()
        {
            var ignoreClasses = new List<string>
            {
                "MergeRequest"
            };

            return typeof(CreateIssueRequest).Assembly.GetExportedTypes().
                Where(t => t.Name.EndsWith("Request", StringComparison.OrdinalIgnoreCase)).
                Where(t => !ignoreClasses.Contains(t.Name)).
                Where(t => t.IsClass);
        }
    }
}
