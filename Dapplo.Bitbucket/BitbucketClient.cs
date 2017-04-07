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
using Dapplo.HttpExtensions.JsonSimple;

#endregion

namespace Dapplo.Bitbucket
{
    /// <summary>
    ///     A Bitbucket client build by using Dapplo.HttpExtensions
    /// </summary>
    public class BitbucketClient : IUserDomain, IProjectDomain, IRepositoryDomain
    {
        /// <summary>
        ///     Password for the basic authentication
        /// </summary>
        private string _password;

        /// <summary>
        ///     User for the basic authentication
        /// </summary>
        private string _user;

        /// <summary>
        ///     Create the ConfluenceApi object, here the HttpClient is configured
        /// </summary>
        /// <param name="bitbucketUri">Base URL, e.g. https://yourConfluenceserver</param>
        /// <param name="httpSettings">IHttpSettings or null for default</param>
        private BitbucketClient(Uri bitbucketUri, IHttpSettings httpSettings = null)
        {
            if (bitbucketUri == null)
            {
                throw new ArgumentNullException(nameof(bitbucketUri));
            }
            BitbucketUri = bitbucketUri;
            BitbucketApiUri = bitbucketUri.AppendSegments("rest", "api", "1.0");

            Behaviour = ConfigureBehaviour(new HttpBehaviour(), httpSettings);
        }

        /// <summary>
        ///     The IHttpBehaviour for this Confluence instance
        /// </summary>
        public IHttpBehaviour Behaviour { get; }

        /// <summary>
        ///     Set Basic Authentication for the current client
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="password">password</param>
        public void SetBasicAuthentication(string user, string password)
        {
            _user = user;
            _password = password;
        }

        /// <summary>
        ///     The base URI for your Confluence server api calls
        /// </summary>
        public Uri BitbucketApiUri { get; }

        /// <summary>
        ///     The base URI for your Confluence server downloads
        /// </summary>
        public Uri BitbucketUri { get; }

        /// <inheritdoc />
        public IUserDomain User => this;

        /// <inheritdoc />
        public IRepositoryDomain Repository => this;

        /// <inheritdoc />
        public IProjectDomain Project => this;

        /// <summary>
        ///     Factory method to create a BitbucketClient
        /// </summary>
        /// <param name="bitbucketUri">Uri to your bitbucket server</param>
        /// <param name="httpSettings">IHttpSettings used if you need specific settings</param>
        /// <returns>IBitbucketClient</returns>
        public static IBitbucketClient Create(Uri bitbucketUri, IHttpSettings httpSettings = null)
        {
            return new BitbucketClient(bitbucketUri, httpSettings);
        }

        /// <summary>
        ///     Helper method to configure the IChangeableHttpBehaviour
        /// </summary>
        /// <param name="behaviour">IChangeableHttpBehaviour</param>
        /// <param name="httpSettings">IHttpSettings</param>
        /// <returns>the behaviour, but configured as IHttpBehaviour </returns>
        private IHttpBehaviour ConfigureBehaviour(IChangeableHttpBehaviour behaviour, IHttpSettings httpSettings = null)
        {
            behaviour.JsonSerializer = new SimpleJsonSerializer();
            behaviour.HttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings.ShallowClone();
            behaviour.OnHttpRequestMessageCreated = httpRequestMessage =>
            {
                httpRequestMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "no-check");
                if (!string.IsNullOrEmpty(_user) && (_password != null))
                {
                    httpRequestMessage?.SetBasicAuthorization(_user, _password);
                }
                return httpRequestMessage;
            };
            return behaviour;
        }
    }
}