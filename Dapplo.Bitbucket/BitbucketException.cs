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

using System.Linq;
using System.Net;
using System.Net.Http;
using Dapplo.Bitbucket.Entities;

namespace Dapplo.Bitbucket
{
    /// <summary>
    /// This exception wraps a HttpRequestException with Bitbucket specifics
    /// </summary>
    public class BitbucketException : HttpRequestException
    {
        /// <summary>
        /// Constructor with a HttpStatus code and an error response
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="response">string with the error response message</param>
        public BitbucketException(HttpStatusCode httpStatusCode, string response) : base($"{httpStatusCode}({(int)httpStatusCode}) : {response}")
        {
        }

        /// <summary>
        ///  Constructor with a HttpStatus code and a list of Error objects
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="errors">IList with Error</param>
        public BitbucketException(HttpStatusCode httpStatusCode, ErrorList errors = null) : base(errors?.Errors?.FirstOrDefault()?.Message ?? $"{httpStatusCode}({(int)httpStatusCode})")
        {
            Errors = errors;
        }

        /// <summary>
        /// Get the errors for the exception
        /// </summary>
        public ErrorList Errors { get; }
    }
}
