using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using FluentAssertions;
using GitLabApiClient.Internal.Http;
using Xunit;

namespace GitLabApiClient.Test.Internal.Http
{
    public class HttpResponseHeadersExtensionsTest
    {
        private static HttpResponseHeaders GetTestHeaders()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Add("X-Fifty-Five", "55");
            response.Headers.Add("X-Empty", "");

            return response.Headers;
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithEmptyValue_ReturnDefaultValue()
        {
            const int expected = 0;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Empty");

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithNotExistingKey_ReturnDefaultValue()
        {
            const int expected = 0;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Not-Exists");

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithValue_ReturnValue()
        {
            const int expected = 55;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Fifty-Five");

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFirstHeaderIntValueOrNull_WithEmptyValue_ReturnNull()
        {
            var sut = GetTestHeaders();

            int? result = sut.GetFirstHeaderIntValueOrNull("X-Empty");

            Assert.Null(result);
        }

        [Fact]
        public void GetFirstHeaderIntValueOrNull_WithNotExistingKey_ReturnNull()
        {
            var sut = GetTestHeaders();

            int? result = sut.GetFirstHeaderIntValueOrNull("X-Not-Exists");

            Assert.Null(result);
        }

        [Fact]
        public void GetFirstHeaderIntValueOrNull_WithValue_ReturnValue()
        {
            const int expectedResult = 55;
            var sut = GetTestHeaders();

            int? result = sut.GetFirstHeaderIntValueOrNull("X-Fifty-Five");

            Assert.True(result.HasValue);
            result.Value.Should().Be(expectedResult);
        }
    }
}
