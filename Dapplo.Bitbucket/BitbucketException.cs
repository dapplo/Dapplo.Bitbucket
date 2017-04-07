using System.Collections.Generic;
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
