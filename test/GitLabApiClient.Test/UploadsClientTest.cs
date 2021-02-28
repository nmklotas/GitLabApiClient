using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Uploads.Requests;
using Xunit;
using static GitLabApiClient.Test.Utilities.GitLabApiHelper;

namespace GitLabApiClient.Test
{
    [Trait("Category", "LinuxIntegration")]
    [Collection("GitLabContainerFixture")]
    public class UploadsClientTest
    {
        private readonly UploadsClient _sut = new UploadsClient(
            GetFacade());

        [Fact]
        public async Task UploadFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("gitlabtest.png"));
            string fileName = $"{Guid.NewGuid()}";
            string fileNameWithExtension = $"{fileName}.png";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                var upload = await _sut.UploadFile(TestProjectTextId, new CreateUploadRequest(stream, fileNameWithExtension));

                upload.Alt.Should().Be(fileName, "Provided Alt tag should only contain the filename without the extension.");
                upload.Url.Should().EndWith(fileNameWithExtension, "Provided Url should end with the filename provided to the upload method.");
                upload.Markdown.Should().Be($"![{fileName}]({upload.Url})", "Provided markdown should start be ![{fileName without extension}](upload url).");
            }
        }
    }
}
