using System;

namespace GitLabApiClient.Models
{
    public enum EpicIssueState
    {
        Opened,
        Closed,
        All
    }
    public static class State
    {
        public static string GetStateQueryValue(EpicIssueState state)
        {
            switch (state)
            {
                case EpicIssueState.Opened:
                    return "opened";
                case EpicIssueState.Closed:
                    return "closed";
                case EpicIssueState.All:
                    return "";
                default:
                    throw new NotSupportedException($"State {state} is not supported");
            }
        }
    }
}
