using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Entities;
using Dapplo.HttpExtensions;

namespace Dapplo.Bitbucket.Internal
{
	/// <summary>
	/// User related methods
	/// </summary>
	internal class UserApi : IUserApi
	{
		private readonly IBitbucketClient _bitbucketClient;

		internal UserApi(IBitbucketClient bitbucketClient)
		{
			_bitbucketClient = bitbucketClient;
		}

		public async Task<User> GetUserAsync(string user, CancellationToken cancellationToken = new CancellationToken())
		{
			_bitbucketClient.PromoteContext();
			var userUri = _bitbucketClient.BitbucketApiUri.AppendSegments("users", user);

			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}
	}
}
