using System;
using System.ComponentModel;

namespace GitLabApiClient.Models.LabelEvents.Responses
{
    public enum EventResourceType
    {
        [Description("issues")]
        Issues,
        [Description("epics")]
        Epics,
        [Description("merge_requests")]
        MergeRequests
    }

    internal static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
