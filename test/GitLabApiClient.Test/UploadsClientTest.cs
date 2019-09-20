using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GitLabApiClient.Models.Uploads.Requests;
using GitLabApiClient.Test.Utilities;
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
                var upload = await _sut.UploadFile(
                    new CreateUploadRequest(TestGroupTextId, stream, fileNameWithExtension));

                upload.Alt.Should().Be(fileName, "Provided Alt tag should only contain the filename without the extension.");
                upload.Alt.Should().EndWith(fileName, "Provided Url should end with the filename provided to the upload method.");
                upload.Alt.Should().EndWith(fileName, "Provided Markdown should end with the filename provided to the upload method.");
                upload.Alt.Should().StartWith($"![{fileName}]", "Provided markdown should start with ![{fileName without extension}].");
            }
        }
    }
}
