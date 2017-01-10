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
	///     The interface to all repository related functionality
	/// </summary>
	public interface IRepositoryApi
	{
		/// <summary>
		///     Create repository in the specified project
		/// </summary>
		/// <param name="projectKey">Key of the project to create a repository for</param>
		/// <param name="newRepository">
		///     Repository object with needed information, following values can be set: Name, IsForkable
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Repository with the information on the created repository</returns>
		Task<Repository> AddAsync(string projectKey, Repository newRepository, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Retrieve branches for repository
		/// </summary>
		/// <param name="projectKey"></param>
		/// <param name="repositorySlug"></param>
		/// <param name="token"></param>
		/// <returns>Results with Branch objects</returns>
		Task<Results<Branch>> GetBranchesAsync(string projectKey, string repositorySlug, CancellationToken token = default(CancellationToken));

		/// <summary>
		///     Retrieve repositories for project
		/// </summary>
		/// <returns>Results with Repository objects</returns>
		Task<Results<Repository>> GetRepositoriesAsync(string projectKey, CancellationToken token = default(CancellationToken));

		/// <summary>
		///     Retrieve commit for project/respository and SHA1
		/// </summary>
		/// <returns>Commit</returns>
		Task<Commit> GetCommitAsync(string projectKey, string repositorySlug, string sha1, CancellationToken token = default(CancellationToken));

		/// <summary>
		///     Get build-status for commit
		/// </summary>
		/// <returns>Results with BuildState objects</returns>
		Task<Results<BuildState>> GetBuildStatus(string changeSetSha, CancellationToken token = default(CancellationToken));

		/// <summary>
		///     Set build-state for commit
		/// </summary>
		/// <returns>BuildStates</returns>
		Task SetBuildStatus(string changeSetSha, BuildState buildState, CancellationToken token = default(CancellationToken));
	}
}