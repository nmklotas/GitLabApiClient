using System;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Projects.Responses;

namespace GitLabApiClient.Internal.Paths
{
    public class ProjectId
    {
        private readonly string _path;

        private ProjectId(string projectPath) => _path = projectPath;

        /// <summary>
        /// ProjectId will encode the project path for you
        /// </summary>
        /// <param name="projectPath">The project path ie. 'group/project'</param>
        /// <returns></returns>
        public static implicit operator ProjectId(string projectPath) => new ProjectId(projectPath.UrlEncode());

        public static implicit operator ProjectId(int projectId) => new ProjectId(projectId.ToString());

        public static implicit operator ProjectId(Project project)
        {
            string projectPath = project.PathWithNamespace?.Trim();
            if (!string.IsNullOrEmpty(projectPath))
                return new ProjectId(projectPath.UrlEncode());

            int id = project.Id;
            if (id > 0)
                return new ProjectId(id.ToString());

            throw new ArgumentException("Cannot determine project path or id from provided Project instance.");
        }

        public override string ToString()
        {
            return _path;
        }
    }
}
