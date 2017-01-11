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
using Dapplo.Bitbucket.Entities;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Bitbucket.Internal
{
	/// <summary>
	///     Repository related methods
	/// </summary>
	internal class ProjectApi : IProjectApi
	{
		private readonly IBitbucketClient _bitbucketClient;

		internal ProjectApi(IBitbucketClient bitbucketClient)
		{
			_bitbucketClient = bitbucketClient;
		}


		public async Task<Results<Project>> GetAllAsync(PagingInfo pagingInfo = null, CancellationToken token = default(CancellationToken))
		{
			var projectsUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects");
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
			_bitbucketClient.PromoteContext();
			var response = await projectsUri.GetAsAsync<HttpResponse<Results<Project>, Error>>(token);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}

			return response.Response;
		}


		/// <inheritdoc />
		public async Task<TBitmap> GetAvatarAsync<TBitmap>(string projectKey, int? size = null, CancellationToken cancellationToken = new CancellationToken())
			where TBitmap : class
		{
			var projectAvatarUri = _bitbucketClient.BitbucketApiUri.AppendSegments("projects", projectKey, "avatar.png");
			if (size.HasValue)
			{
				projectAvatarUri = projectAvatarUri.ExtendQuery("s", size.Value);
			}

			_bitbucketClient.PromoteContext();
			return await projectAvatarUri.GetAsAsync<TBitmap>(cancellationToken).ConfigureAwait(false);
		}
	}
}