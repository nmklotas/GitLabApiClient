using System;
using GitLabApiClient.Internal.Http;

namespace GitLabApiClient;

public record PageOptions(int Page)
{
    private readonly int? _itemsPerPage;

    public int ItemsPerPage
    {
        get => _itemsPerPage ?? GitLabApiPagedRequestor.MaxItemsPerPage;
        init
        {
            if (value > GitLabApiPagedRequestor.MaxItemsPerPage)
            {
                throw new ArgumentException($"Max items per page is {GitLabApiPagedRequestor.MaxItemsPerPage}");
            }

            _itemsPerPage = value;
        }
    }
}
