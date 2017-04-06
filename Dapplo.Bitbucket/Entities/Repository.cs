﻿#region Dapplo 2016 - GNU Lesser General Public License

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
    /// Information on a repository
    /// </summary>
    [DataContract]
    public class Repository
    {
        public Repository()
        {
            ScmId = "git";
        }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "scmId", EmitDefaultValue = false)]
        public string ScmId { get; set; }

        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(Name = "statusMessage", EmitDefaultValue = false)]
        public string StatusMessage { get; set; }

        [DataMember(Name = "forkable", EmitDefaultValue = false)]
        public bool IsForkable { get; set; }

        [DataMember(Name = "project", EmitDefaultValue = false)]
        public Project Project { get; set; }

        [DataMember(Name = "public", EmitDefaultValue = false)]
        public bool IsPublic { get; set; }

        [DataMember(Name = "cloneUrl", EmitDefaultValue = false)]
        public string CloneUrl { get; set; }

        [DataMember(Name = "link", EmitDefaultValue = false)]
        public Link Link { get; set; }

        [DataMember(Name = "links", EmitDefaultValue = false)]
        public Links Links { get; set; }
    }
}