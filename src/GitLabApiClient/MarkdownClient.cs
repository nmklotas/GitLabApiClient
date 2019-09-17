using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitLabApiClient.Http;
using GitLabApiClient.Models.Markdown.Request;
using GitLabApiClient.Models.Markdown.Response;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to render a markdown document.
    /// </summary>
    public sealed class MarkdownClient
    {
        private readonly IGitLabHttpFacade _httpFacade;

        internal MarkdownClient(IGitLabHttpFacade httpFacade) => 
            _httpFacade = httpFacade;

        public async Task<Markdown> RenderAsync(RenderMarkdownRequest request) =>
            await _httpFacade.Post<Markdown>($"markdown", request);
    }
}
