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

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Entities;

#endregion

namespace Dapplo.Bitbucket
{
	/// <summary>
	///     The interface to all project related functionality
	/// </summary>
	public interface IProjectApi
	{
		
		/// <summary>
		///     Retrieve all projects which the current user is allowed to see
		/// </summary>
		/// <returns>Results with projects</returns>
		Task<Results<Project>> GetAllAsync(CancellationToken token = default(CancellationToken));
	}
}