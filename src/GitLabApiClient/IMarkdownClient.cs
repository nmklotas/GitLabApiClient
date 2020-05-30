using System.Threading.Tasks;
using GitLabApiClient.Models.Markdown.Request;
using GitLabApiClient.Models.Markdown.Response;

namespace GitLabApiClient
{
    public interface IMarkdownClient
    {
        Task<Markdown> RenderAsync(RenderMarkdownRequest request);
    }
}
