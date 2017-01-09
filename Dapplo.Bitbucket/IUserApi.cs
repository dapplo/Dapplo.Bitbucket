#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Entities;

#endregion

namespace Dapplo.Bitbucket
{
	/// <summary>
	///     The interface to all user related functionality
	/// </summary>
	public interface IUserApi
	{
		/// <summary>
		///     Get user information
		/// </summary>
		/// <param name="user">Username</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		Task<User> GetUserAsync(string user, CancellationToken cancellationToken = default(CancellationToken));
	}
}