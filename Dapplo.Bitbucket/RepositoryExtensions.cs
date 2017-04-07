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
using Dapplo.Bitbucket.Domains;
using Dapplo.Bitbucket.Entities;
using Dapplo.Bitbucket.Internal;
using Dapplo.HttpExtensions;
using System.Collections.Generic;

#endregion

namespace Dapplo.Bitbucket
{
    /// <summary>
    ///     Repository related methods
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        ///     Create repository in the specified project
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="projectKey">Key of the project to create a repository for</param>
        /// <param name="newRepository">
        ///     Repository object with needed information, following values can be set: Name, IsForkable
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Repository with the information on the created repository</returns>
        public static async Task<Repository> AddAsync(this IRepositoryDomain bitbucketClient, string projectKey, Repository newRepository, CancellationToken cancellationToken = default(CancellationToken))
        {
            var repoUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos");
            bitbucketClient.Behaviour.MakeCurrent();

            var response = await repoUri.PostAsync<HttpResponse<Repository, ErrorList>>(newRepository, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Retrieve branches for repository
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="projectKey">Project key</param>
        /// <param name="repositorySlug">Slug for repository</param>
        /// <param name="pagingInfo">optional PagingInfo, the result is PagingInfo</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Branches</returns>
        public static async Task<Results<Branch>> GetBranchesAsync(this IRepositoryDomain bitbucketClient, string projectKey, string repositorySlug, PagingInfo pagingInfo = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var branchesUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos", repositorySlug, "branches");
            if (pagingInfo != null)
            {
                if (pagingInfo.IsLastPage)
                {
                    return new Results<Branch>()
                    {
                        IsLastPage = true,
                        Values = new List<Branch>()
                    };
                }
                branchesUri = branchesUri.ExtendQuery("start", pagingInfo.NextPageStart);
                branchesUri = branchesUri.ExtendQuery("limit", pagingInfo.Limit);
            }
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await branchesUri.GetAsAsync<HttpResponse<Results<Branch>, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Retrieve repositories for project
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="projectKey">Project key</param>
        /// <param name="pagingInfo">optional PagingInfo, the result is PagingInfo</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with Repository objects</returns>
        public static async Task<Results<Repository>> GetAllAsync(this IRepositoryDomain bitbucketClient, string projectKey, PagingInfo pagingInfo = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var repositoriesUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos");
            if (pagingInfo != null)
            {
                if (pagingInfo.IsLastPage)
                {
                    return new Results<Repository>()
                    {
                        IsLastPage = true,
                        Values = new List<Repository>()
                    };
                }
                repositoriesUri = repositoriesUri.ExtendQuery("start", pagingInfo.NextPageStart);
                repositoriesUri = repositoriesUri.ExtendQuery("limit", pagingInfo.Limit);
            }
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await repositoriesUri.GetAsAsync<HttpResponse<Results<Repository>, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Retrieve commit for project/respository and SHA1
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="projectKey">key of the project</param>
        /// <param name="repositorySlug">Name of the repo</param>
        /// <param name="sha1">string with the sha1 of the commit</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Commit</returns>
        public static async Task<Commit> GetCommitAsync(this IRepositoryDomain bitbucketClient, string projectKey, string repositorySlug, string sha1, CancellationToken cancellationToken = default(CancellationToken))
        {
            var commitUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos", repositorySlug, "commits", sha1);
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await commitUri.GetAsAsync<HttpResponse<Commit, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Retrieve commit for project/respository and SHA1
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="projectKey">key of the project</param>
        /// <param name="repositorySlug">Name of the repo</param>
        /// <param name="pagingInfo">optional PagingInfo, the result is also PagingInfo</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with commit</returns>
        public static async Task<Results<Commit>> GetCommitsAsync(this IRepositoryDomain bitbucketClient, string projectKey, string repositorySlug, PagingInfo pagingInfo = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var commitsUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "repos", repositorySlug, "commits");
            if (pagingInfo != null)
            {
                if (pagingInfo.IsLastPage)
                {
                    return new Results<Commit>()
                    {
                        IsLastPage = true,
                        Values = new List<Commit>()
                    };
                }
                commitsUri = commitsUri.ExtendQuery("start", pagingInfo.NextPageStart);
                commitsUri = commitsUri.ExtendQuery("limit", pagingInfo.Limit);
            }
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await commitsUri.GetAsAsync<HttpResponse<Results<Commit>, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get build-status for commit
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="changeSetSha">Sha for the commit</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>BuildStates</returns>
        public static async Task<Results<BuildState>> GetBuildStatus(this IRepositoryDomain bitbucketClient, string changeSetSha, CancellationToken cancellationToken = default(CancellationToken))
        {
            var commitUri = bitbucketClient.BitbucketApiUri.AppendSegments("commits", changeSetSha);
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await commitUri.GetAsAsync<HttpResponse<Results<BuildState>, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Set build-state for commit
        /// </summary>
        /// <param name="bitbucketClient">The IRepositoryDomain to pin this extension method to</param>
        /// <param name="changeSetSha">string with sha to set the build state for</param>
        /// <param name="buildState">BuildState</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task SetBuildStatus(this IRepositoryDomain bitbucketClient, string changeSetSha, BuildState buildState, CancellationToken cancellationToken = default(CancellationToken))
        {
            var commitUri = bitbucketClient.BitbucketApiUri.AppendSegments("commits", changeSetSha);
            bitbucketClient.Behaviour.MakeCurrent();
            await commitUri.PostAsync(buildState, cancellationToken).ConfigureAwait(false);
        }
    }
}