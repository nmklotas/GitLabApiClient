﻿using System;
using System.Collections.Generic;

namespace GitLabApiClient.Models.Groups.Requests
{
	/// <summary>
	/// Options for Groups listing
	/// </summary>
	public class GroupsQueryOptions
    {
		/// <summary>
		/// Skip the group IDs passes
		/// </summary>
		public IList<int> SkipGroups { get; set; } = new List<int>();

		/// <summary>
		/// Show all the groups you have access to
		/// </summary>
		public bool AllAvailable { get; set; }

		/// <summary>
		/// Return list of authorized groups matching the search criteria
		/// </summary>
		public string Search { get; set; }

		/// <summary>
		/// Order groups by name or path. Default is name
		/// </summary>
		public GroupsOrder Order { get; set; }

		/// <summary>
		/// Order groups in asc or desc order. Default is asc
		/// </summary>
		public GroupsSort Sort { get; set; }

		/// <summary>
		/// Include group statistics (admins only)
		/// </summary>
		public bool Statistics { get; set; }

		/// <summary>
		/// Limit by groups owned by the current user
		/// </summary>
		public bool Owned { get; set; }

    }
}
