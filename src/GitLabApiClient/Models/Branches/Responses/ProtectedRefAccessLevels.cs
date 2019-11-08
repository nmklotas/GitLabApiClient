using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Branches.Responses
{
    public enum ProtectedRefAccessLevels
    {
        NO_ACCESS = 0,
        DEVELOPER_ACCESS = 30,
        MAINTAINER_ACCESS = 40,
        ADMIN_ACCESS = 60
    }
}
