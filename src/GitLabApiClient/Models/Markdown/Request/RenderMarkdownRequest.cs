using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Markdown.Request
{
    /// <summary>
    /// Used to render a markdown document
    /// </summary>
    public sealed class RenderMarkdownRequest
    {
        /// <summary>
        /// The markdown text to render
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Render text using GitLab Flavored Markdown
        /// </summary>
        [JsonProperty("gfm")]
        public bool? FlavoredMarkdown { get; set; } = false;

        /// <summary>
        /// Use as a context when creating references using GitLab Flavored Markdown. Authentication is required if a project is not public.
        /// </summary>
        [JsonProperty("project")]
        public string Project { get; set; }

        public RenderMarkdownRequest(string text)
        {
            Guard.NotEmpty(text, nameof(text));
            Text = text;
        }
    }
}
