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
using Dapplo.Bitbucket.Domains;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Bitbucket
{
    /// <summary>
    ///     The is the interface to the base client functionality of the Confluence API
    /// </summary>
    public interface IBitbucketClient
    {
        /// <summary>
        ///     The base URI for your Bitbucket server api calls
        /// </summary>
        Uri BitbucketApiUri { get; }

        /// <summary>
        ///     The base URI for your Bitbucket server downloads
        /// </summary>
        Uri BitbucketUri { get; }

        /// <summary>
        ///     Methods in the user domain
        /// </summary>
        IUserDomain User { get; }

        /// <summary>
        ///     Methods in the user domain
        /// </summary>
        IProjectDomain Project { get; }

        /// <summary>
        ///     Methods in the repository domain
        /// </summary>
        IRepositoryDomain Repository { get; }

        ///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
        ///     HttpClient which works with Confluence
        IHttpBehaviour Behaviour { get; }

        /// <summary>
        ///     Enables basic authentication for every request following this call
        /// </summary>
        /// <param name="user">string with the confluence user</param>
        /// <param name="password">string with the password for the confluence user</param>
        void SetBasicAuthentication(string user, string password);
    }
}