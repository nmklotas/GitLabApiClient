using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Groups.Responses;

namespace GitLabApiClient.Internal.Paths
{
    public class GroupId
    {
        private readonly string _path;

        private GroupId(string groupPath) => _path = groupPath;

        /// <summary>
        /// GroupId will encode the group path for you
        /// </summary>
        /// <param name="groupPath">The project path ie. 'group/project'</param>
        /// <returns></returns>
        public static implicit operator GroupId(string groupPath)
        {
            return new GroupId(groupPath.UrlEncode());
        }

        public static implicit operator GroupId(int groupId)
        {
            return new GroupId(groupId.ToString());
        }

        public static implicit operator GroupId(Group group)
        {
            string groupPath = group.FullPath?.Trim();
            if (!string.IsNullOrEmpty(groupPath))
                return new GroupId(groupPath.UrlEncode());

            int id = group.Id;
            if (id > 0)
                return new GroupId(id.ToString());

            throw new ArgumentException("Cannot determine group path or id from provided Group instance.");
        }

        public override string ToString()
        {
            return _path;
        }
    }
}
