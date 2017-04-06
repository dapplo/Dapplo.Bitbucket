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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Domains;
using Dapplo.Bitbucket.Entities;
using Dapplo.Bitbucket.Internal;
using Dapplo.HttpExtensions;
using Dapplo.Log;

#endregion

namespace Dapplo.Bitbucket
{
    /// <summary>
    ///     Repository related methods
    /// </summary>
    public static class ProjectExtensions
    {
        /// <summary>
        ///     Retrieve all projects which the current user is allowed to see
        /// </summary>
        /// <param name="bitbucketClient"></param>
        /// <param name="pagingInfo">PagingInfo to enable paging, the Results object is of type PagingInfo</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with projects</returns>
        public static async Task<Results<Project>> GetAllAsync(this IProjectDomain bitbucketClient, PagingInfo pagingInfo = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var projectsUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects");
            if (pagingInfo != null)
            {
                if (pagingInfo.IsLastPage)
                {
                    return new Results<Project>()
                    {
                        IsLastPage = true,
                        Values = new List<Project>()
                    };
                }
                projectsUri = projectsUri.ExtendQuery("start", pagingInfo.NextPageStart);
                projectsUri = projectsUri.ExtendQuery("limit", pagingInfo.Limit);
            }
            bitbucketClient.Behaviour.MakeCurrent();
            var response = await projectsUri.GetAsAsync<HttpResponse<Results<Project>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }


        /// <summary>
        /// Retrieve the Avatar for the specified project
        /// </summary>
        /// <typeparam name="TBitmap">Type of the bitmap, e.g. Bitmap or BitmapSource</typeparam>
        /// <param name="bitbucketClient">IProjectDomain to pin this extension method to</param>
        /// <param name="projectKey">string with key of the project</param>
        /// <param name="size">wanted size of the avatar</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>TBitmap</returns>
        public static async Task<TBitmap> GetAvatarAsync<TBitmap>(this IProjectDomain bitbucketClient, string projectKey, int? size = null, CancellationToken cancellationToken = new CancellationToken())
            where TBitmap : class
        {
            var projectAvatarUri = bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "avatar.png");
            if (size.HasValue)
            {
                projectAvatarUri = projectAvatarUri.ExtendQuery("s", size.Value);
            }

            bitbucketClient.Behaviour.MakeCurrent();
            return await projectAvatarUri.GetAsAsync<TBitmap>(cancellationToken).ConfigureAwait(false);
        }
    }
}