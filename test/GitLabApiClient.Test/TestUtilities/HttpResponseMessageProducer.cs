using System.Net;
using System.Net.Http;

namespace GitLabApiClient.Test.TestUtilities
{
    public class HttpResponseMessageProducer
    {
        public static HttpResponseMessage Success(string content)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(content)};
            return response;
        }
    }
}