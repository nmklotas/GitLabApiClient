using System;

namespace GitLabApiClient.Models.Issues.Requests
{
    public enum EpicsIssuesOrder

    {
        CreatedAt,
        UpdatedAt
    }

    public static class Order
    {
        public static string GetIssuesOrderQueryValue(EpicsIssuesOrder order)
        {
            switch (order)
            {
                case EpicsIssuesOrder.CreatedAt:
                    return "created_at";
                case EpicsIssuesOrder.UpdatedAt:
                    return "updated_at";
                default:
                    throw new NotSupportedException($"Order {order} is not supported");
            }
        }
    }
}
