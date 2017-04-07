#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Bitbucket
// 
// Dapplo.Bitbucket is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Bitbucket is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Bitbucket. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Bitbucket.Entities
{
    /// <summary>
    /// Information on a branch
    /// </summary>
    [DataContract]
    public class Branch
    {
        /// <summary>
        /// ID of the branch
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// ID to display
        /// </summary>
        [DataMember(Name = "displayId", EmitDefaultValue = false)]
        public string DisplayId { get; set; }

        /// <summary>
        /// The SHA for the most recent commit
        /// </summary>
        [DataMember(Name = "latestChangeset", EmitDefaultValue = false)]
        public string LatestChangeset { get; set; }

        /// <summary>
        /// Is this the default branch?
        /// </summary>
        [DataMember(Name = "isDefault", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }
    }
}