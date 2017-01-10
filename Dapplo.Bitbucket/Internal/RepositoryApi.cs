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

using System;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Entities;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Bitbucket.Internal
{
	/// <summary>
	///     Repository related methods
	/// </summary>
	internal class RepositoryApi : IRepositoryApi
	{
		private readonly IBitbucketClient _bitbucketClient;

		internal RepositoryApi(IBitbucketClient bitbucketClient)
		{
			_bitbucketClient = bitbucketClient;
		}

		/// <inheritdoc />
		public async Task<Repository> AddAsync(string projectKey, Repository newRepository, CancellationToken token = default(CancellationToken))
		{
			var repoUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos");
			_bitbucketClient.PromoteContext();

			var response = await repoUri.PostAsync<HttpResponse<Repository, Error>>(newRepository, token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Retrieve branches for repository
		/// </summary>
		/// <param name="projectKey"></param>
		/// <param name="repositorySlug"></param>
		/// <param name="token"></param>
		/// <returns>Branches</returns>
		public async Task<Results<Branch>> GetBranchesAsync(string projectKey, string repositorySlug, CancellationToken token = default(CancellationToken))
		{
			var branchesUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos", repositorySlug, "branches");
			_bitbucketClient.PromoteContext();
			var response = await branchesUri.GetAsAsync<HttpResponse<Results<Branch>, Error>>(token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}

			return response.Response;
		}

		/// <summary>
		///     Retrieve repositories for project
		/// </summary>
		/// <returns>Results with Repository objects</returns>
		public async Task<Results<Repository>> GetRepositoriesAsync(string projectKey, CancellationToken token = default(CancellationToken))
		{
			var repositoriesUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos");
			_bitbucketClient.PromoteContext();
			var response = await repositoriesUri.GetAsAsync<HttpResponse<Results<Repository>, Error>>(token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}

			return response.Response;
		}

		/// <summary>
		///     Retrieve commit for project/respository and SHA1
		/// </summary>
		/// <returns>Commit</returns>
		public async Task<Commit> GetCommitAsync(string projectKey, string repositorySlug, string sha1, CancellationToken token = default(CancellationToken))
		{
			var commitUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos", repositorySlug, "commits", sha1);
			_bitbucketClient.PromoteContext();
			var response = await commitUri.GetAsAsync<HttpResponse<Commit, Error>>(token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}

			return response.Response;
		}

		/// <summary>
		///     Get build-status for commit
		/// </summary>
		/// <returns>BuildStates</returns>
		public async Task<Results<BuildState>> GetBuildStatus(string changeSetSha, CancellationToken token = default(CancellationToken))
		{
			var commitUri = _bitbucketClient.BitbucketApiUri.AppendSegments("commits", changeSetSha);
			_bitbucketClient.PromoteContext();
			var response = await commitUri.GetAsAsync<HttpResponse<Results<BuildState>, Error>>(token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}

			return response.Response;
		}

		/// <summary>
		///     Set build-state for commit
		/// </summary>
		/// <returns>BuildStates</returns>
		public async Task SetBuildStatus(string changeSetSha, BuildState buildState, CancellationToken token = default(CancellationToken))
		{
			var commitUri = _bitbucketClient.BitbucketApiUri.AppendSegments("commits", changeSetSha);
			_bitbucketClient.PromoteContext();
			await commitUri.PostAsync(buildState, token);
		}
	}
}