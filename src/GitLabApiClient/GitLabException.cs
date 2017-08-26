using System;

namespace GitLabApiClient
{
    [Serializable]
    public class GitLabException : Exception
    {
        public GitLabException()
        {
        }

        public GitLabException(string message) : base(message)
        {
        }

        public GitLabException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}