using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Used to create labels in a group.
    /// </summary>
    public sealed class CreateGroupLabelRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroupLabelRequest"/> class.
        /// </summary>
        /// <param name="name">Name of the label.</param>
        public CreateGroupLabelRequest(string name)
        {
            Guard.NotEmpty(name, nameof(name));
            Name = name;
        }

        /// <summary>
        /// The name of the label.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The color of the label given in 6-digit hex notation with leading ‘#’ sign (e.g. #FFAABB) or one of the CSS color names.
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// The description of the label.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
