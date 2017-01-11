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
	///     User related methods
	/// </summary>
	internal class UserApi : IUserApi
	{
		private readonly IBitbucketClient _bitbucketClient;

		internal UserApi(IBitbucketClient bitbucketClient)
		{
			_bitbucketClient = bitbucketClient;
		}

		/// <inheritdoc />
		public async Task<Results<User>> GetAllAsync(PagingInfo pagingInfo = null, CancellationToken cancellationToken = new CancellationToken())
		{
			_bitbucketClient.PromoteContext();
			var usersUri = _bitbucketClient.BitbucketApiUri.AppendSegments("users");

			var response = await usersUri.GetAsAsync<HttpResponse<Results<User>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<User> GetAsync(string userSlug, CancellationToken cancellationToken = new CancellationToken())
		{
			_bitbucketClient.PromoteContext();
			var userUri = _bitbucketClient.BitbucketApiUri.AppendSegments("users", userSlug);

			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<TBitmap> GetAvatarAsync<TBitmap>(string userslug, int? size = null, CancellationToken cancellationToken = new CancellationToken())
			where TBitmap : class
		{
			_bitbucketClient.PromoteContext();
			// This doesn't use the API itself!
			var avatarUri = _bitbucketClient.BitbucketUri.AppendSegments("users", userslug, "avatar.png");
			if (size.HasValue)
			{
				avatarUri = avatarUri.ExtendQuery("s", size.Value);
			}
			return await avatarUri.GetAsAsync<TBitmap>(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<TBitmap> GetAvatarAsync<TBitmap>(User user, int? size = null, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
		{
			return await GetAvatarAsync<TBitmap>(user.Slug, size, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task ChangeAvatarAsync<TBitmap>(string userslug, TBitmap avatar, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
		{
			_bitbucketClient.PromoteContext();
			var avatarUri = _bitbucketClient.BitbucketUri.AppendSegments("users", userslug, "avatar.png");
			await avatarUri.PostAsync(avatar, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task ChangeAvatarAsync<TBitmap>(User user, TBitmap avatar, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
		{
			await ChangeAvatarAsync(user.Slug, avatar, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task DeleteAvatarAsync(string userslug, CancellationToken cancellationToken = new CancellationToken())
		{
			_bitbucketClient.PromoteContext();
			var avatarUri = _bitbucketClient.BitbucketUri.AppendSegments("users", userslug, "avatar.png");
			await avatarUri.DeleteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task DeleteAvatarAsync(User user, CancellationToken cancellationToken = new CancellationToken())
		{
			await DeleteAvatarAsync(user.Slug, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<IDictionary<string, object>> GetSettingsAsync(string userSlug, CancellationToken cancellationToken = new CancellationToken())
		{
			_bitbucketClient.PromoteContext();
			var settingsUri = _bitbucketClient.BitbucketUri.AppendSegments("users", userSlug, "settings");
			return await settingsUri.GetAsAsync<IDictionary<string, object>>(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<IDictionary<string, object>> GetSettingsAsync(User user, CancellationToken cancellationToken = new CancellationToken())
		{
			return await GetSettingsAsync(user.Slug, cancellationToken).ConfigureAwait(false);
		}
	}
}