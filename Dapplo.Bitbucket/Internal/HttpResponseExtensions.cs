using System.Net;
using Dapplo.Bitbucket.Entities;
using Dapplo.HttpExtensions;
using Dapplo.Log;

namespace Dapplo.Bitbucket.Internal
{
    /// <summary>
    /// Extensions for the HttpResponse object
    /// </summary>
    internal static class HttpResponseExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        /// Helper method to log the error
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="error">Error</param>
        private static void LogError(HttpStatusCode httpStatusCode, Error error = null)
        {
            // Log all error information
            Log.Warn().WriteLine("Http status code: {0} ({1}). Response from server: {2}", httpStatusCode.ToString(), (int)httpStatusCode, error?.Message ?? httpStatusCode.ToString());
            if (error?.Context != null)
            {
                Log.Warn().WriteLine("Context {0}", error.Context);
                return;
            }
            if (error?.ExceptionName != null)
            {
                Log.Warn().WriteLine("Exception name {0}", error.ExceptionName);
            }
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <typeparam name="TError">Type for the error content</typeparam>
        /// <param name="expectedHttpStatusCode">optional HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse, TError>(this HttpResponse<TResponse, TError> response, HttpStatusCode? expectedHttpStatusCode = null)
            where TResponse : class where TError : Error
        {
            if (expectedHttpStatusCode.HasValue)
            {
                if (response.StatusCode == expectedHttpStatusCode.Value)
                {
                    return response.Response;
                }
            }
            else if (!response.HasError)
            {
                return response.Response;
            }

            // Log all error information
            LogError(response.StatusCode, response.ErrorResponse);
            throw new BitbucketException(response.StatusCode, response.ErrorResponse);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse>(this HttpResponse<TResponse> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
            where TResponse : class
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return response.Response;
            }
            LogError(response.StatusCode);
            throw new BitbucketException(response.StatusCode);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode(this HttpResponse response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return;
            }
            LogError(response.StatusCode);
            throw new BitbucketException(response.StatusCode);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <typeparam name="TError">Type for the error</typeparam>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode<TError>(this HttpResponseWithError<TError> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK) where TError : Error
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return;
            }
            LogError(response.StatusCode, response.ErrorResponse);
            throw new BitbucketException(response.StatusCode, response.ErrorResponse);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode(this HttpResponseWithError<string> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return;
            }
            Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, response.ErrorResponse);
            throw new BitbucketException(response.StatusCode, response.ErrorResponse);
        }
    }
}
