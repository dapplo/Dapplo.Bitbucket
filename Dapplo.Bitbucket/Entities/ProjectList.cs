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

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class ProjectList
	{
		[DataMember(Name = "size", EmitDefaultValue = false)]
		public int Size { get; set; }

		[DataMember(Name = "limit", EmitDefaultValue = false)]
		public int Limit { get; set; }

		[DataMember(Name = "isLastPage", EmitDefaultValue = false)]
		public bool IsLastPage { get; set; }

		[DataMember(Name = "values", EmitDefaultValue = false)]
		public IList<Project> Projects { get; set; }

		[DataMember(Name = "start", EmitDefaultValue = false)]
		public int Start { get; set; }
	}
}