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

#endregion

namespace Dapplo.Bitbucket
{
    /// <summary>
    ///     User related methods
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        ///     Get all users
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="pagingInfo">PagingInfo to get the next page, this should simply be the results object</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with User instances</returns>
        public static async Task<Results<User>> GetAllAsync(this IUserDomain bitbucketClient, PagingInfo pagingInfo = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var usersUri = bitbucketClient.BitbucketApiUri.AppendSegments("users");

            bitbucketClient.Behaviour.MakeCurrent();
            var response = await usersUri.GetAsAsync<HttpResponse<Results<User>, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get user information
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="userSlug">string with userSlug (this might not be the username)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetAsync(this IUserDomain bitbucketClient, string userSlug, CancellationToken cancellationToken = new CancellationToken())
        {
            var userUri = bitbucketClient.BitbucketApiUri.AppendSegments("users", userSlug);

            bitbucketClient.Behaviour.MakeCurrent();
            var response = await userUri.GetAsAsync<HttpResponse<User, ErrorList>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get avatar for the user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="userslug">string with userSlug (this might not be the username)</param>
        /// <param name="size">optional size</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Bitmap or ImageSource</returns>
        public static async Task<TBitmap> GetAvatarAsync<TBitmap>(this IUserDomain bitbucketClient, string userslug, int? size = null, CancellationToken cancellationToken = new CancellationToken())
            where TBitmap : class
        {
            // This doesn't use the API itself!
            var avatarUri = bitbucketClient.BitbucketUri.AppendSegments("users", userslug, "avatar.png");
            if (size.HasValue)
            {
                avatarUri = avatarUri.ExtendQuery("s", size.Value);
            }
            bitbucketClient.Behaviour.MakeCurrent();
            return await avatarUri.GetAsAsync<TBitmap>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///     Get avatar for the user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="user">User</param>
        /// <param name="size">optional size</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Bitmap or ImageSource</returns>
        public static async Task<TBitmap> GetAvatarAsync<TBitmap>(this IUserDomain bitbucketClient, User user, int? size = null, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
        {
            return await GetAvatarAsync<TBitmap>(bitbucketClient, user.Slug, size, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the avatar for the specified user
        /// </summary>
        /// <typeparam name="TBitmap">Type for the avatar bitmap container object. e.g. MemoryStream, Bitmap, BitmapSource</typeparam>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="userslug">string with userslug</param>
        /// <param name="avatar">the container with the avatar information</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task ChangeAvatarAsync<TBitmap>(this IUserDomain bitbucketClient, string userslug, TBitmap avatar, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
        {
            bitbucketClient.Behaviour.MakeCurrent();
            var avatarUri = bitbucketClient.BitbucketUri.AppendSegments("users", userslug, "avatar.png");
            await avatarUri.PostAsync(avatar, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the avatar for the specified user
        /// </summary>
        /// <typeparam name="TBitmap">Type for the avatar bitmap container object. e.g. MemoryStream, Bitmap, BitmapSource</typeparam>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="user">User</param>
        /// <param name="avatar">the container with the avatar information</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task ChangeAvatarAsync<TBitmap>(this IUserDomain bitbucketClient, User user, TBitmap avatar, CancellationToken cancellationToken = new CancellationToken()) where TBitmap : class
        {
            await ChangeAvatarAsync(bitbucketClient, user.Slug, avatar, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the avatar for the specified user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="userSlug">Slug from the User object</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAvatarAsync(this IUserDomain bitbucketClient, string userSlug, CancellationToken cancellationToken = new CancellationToken())
        {
            bitbucketClient.Behaviour.MakeCurrent();
            var avatarUri = bitbucketClient.BitbucketUri.AppendSegments("users", userSlug, "avatar.png");
            await avatarUri.DeleteAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the avatar for the specified user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="user">User</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAvatarAsync(this IUserDomain bitbucketClient, User user, CancellationToken cancellationToken = new CancellationToken())
        {
            await DeleteAvatarAsync(bitbucketClient, user.Slug, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the settings for the specified user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="userSlug">Slug from the User object</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>IDictionary with name value pairs</returns>
        public static async Task<IDictionary<string, object>> GetSettingsAsync(this IUserDomain bitbucketClient, string userSlug, CancellationToken cancellationToken = new CancellationToken())
        {
            bitbucketClient.Behaviour.MakeCurrent();
            var settingsUri = bitbucketClient.BitbucketUri.AppendSegments("users", userSlug, "settings");
            return await settingsUri.GetAsAsync<IDictionary<string, object>>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the settings for the specified user
        /// </summary>
        /// <param name="bitbucketClient">IUserDomain to pin the extension method to</param>
        /// <param name="user">User</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>IDictionary with name value pairs</returns>
        public static async Task<IDictionary<string, object>> GetSettingsAsync(this IUserDomain bitbucketClient, User user, CancellationToken cancellationToken = new CancellationToken())
        {
            return await GetSettingsAsync(bitbucketClient, user.Slug, cancellationToken).ConfigureAwait(false);
        }
    }
}