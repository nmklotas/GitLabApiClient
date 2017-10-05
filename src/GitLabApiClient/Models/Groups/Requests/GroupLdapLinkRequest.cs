using System;
using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;
namespace GitLabApiClient.Models.Groups.Requests
{
    public sealed class GroupLdapLinkRequest
    {
        public GroupLdapLinkRequest(string id, string cn, string groupAccess, string provider)
        {
            Guard.NotEmpty(id, nameof(id));
            Guard.NotEmpty(cn, nameof(cn));
            Guard.NotEmpty(groupAccess, nameof(groupAccess));
            Guard.NotEmpty(provider, nameof(provider));
            Id = id;
            Cn = cn;
            GroupAccess = groupAccess;
            Provider = provider;
        }

		/// <summary>
		/// The ID of a group
		/// </summary>
		[JsonProperty("id")]
        public string Id { get; }

		/// <summary>
		/// The CN of a LDAP group
		/// </summary>
		[JsonProperty("cn")]
        public string Cn { get; }

		/// <summary>
		/// Minimum access level for members of the LDAP group
		/// </summary>
		[JsonProperty("group_access")]
        public string GroupAccess { get; }

		/// <summary>
		/// LDAP provider for the LDAP group (when using several providers)
		/// </summary>
		[JsonProperty("provider")]
        public string Provider { get; }

    }
}
