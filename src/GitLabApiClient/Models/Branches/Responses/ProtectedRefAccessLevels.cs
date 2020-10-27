using System;
using System.Collections.Generic;
using System.Text;

namespace GitLabApiClient.Models.Branches.Responses
{
    public enum ProtectedRefAccessLevels
    {
        NoAccess = 0,
        DeveloperAccess = 30,
        MaintainerAccess = 40,
        AdminAccess = 60
    }
}
