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
        private HttpResponseHeaders GetTestHeaders()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Add("X-Fifty-Five", "55");
            response.Headers.Add("X-Empty", "");

            return response.Headers;
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithEmptyValue_ReturnDefaultValue()
        {
            int expected = 0;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Empty");

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithNotExistingKey_ReturnDefaultValue()
        {
            int expected = 0;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Not-Exists");

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFirstHeaderValueOrDefault_WithValue_ReturnValue()
        {
            int expected = 55;
            var sut = GetTestHeaders();

            int result = sut.GetFirstHeaderValueOrDefault<int>("X-Fifty-Five");

            result.Should().Be(expected);
        }
    }
}
