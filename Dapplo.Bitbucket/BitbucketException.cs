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
        ///  Constructor with a HttpStatus code and an Error object
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="error">Error</param>
        public BitbucketException(HttpStatusCode httpStatusCode, Error error = null) : base(error?.Message ?? $"{httpStatusCode}({(int)httpStatusCode})")
        {
            Context = error?.Context;
            ExceptionName = error?.ExceptionName;
        }

        /// <summary>
        /// Get the context of the exception
        /// </summary>
        public string Context { get; }

        /// <summary>
        /// Get the exception name
        /// </summary>
        public string ExceptionName { get; }
    }
}
