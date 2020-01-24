using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

namespace GitLabApiClient.Models.Groups.Requests
{
    /// <summary>
    /// Updates an existing label with new name or new color. At least one parameter is required, to update the label.
    /// </summary>
    public sealed class UpdateGroupLabelRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupLabelRequest"/> class.
        /// <param name="name">Old name of the label.</param>
        /// <param name="newName">The new name of the label.</param>
        /// </summary>
        public static UpdateGroupLabelRequest FromNewName(string name, string newName)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(newName, nameof(newName));
            return new UpdateGroupLabelRequest
            {
                Name = name,
                NewName = newName
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupLabelRequest"/> class.
        /// <param name="name">Old name of the label.</param>
        /// <param name="color">The color of the label given in 6-digit hex notation with leading ‘#’ sign (e.g. #FFAABB) or one of the CSS color names.</param>
        /// </summary>
        public static UpdateGroupLabelRequest FromColor(string name, string color)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(color, nameof(color));
            return new UpdateGroupLabelRequest
            {
                Name = name,
                Color = color
            };
        }

        private UpdateGroupLabelRequest() { }

        /// <summary>
        /// The name of the existing label.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The new name of the label
        /// </summary>
        [JsonProperty("new_name")]
        public string NewName { get; private set; }

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
