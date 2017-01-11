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
using System.Drawing;
using System.Threading.Tasks;
using Dapplo.Bitbucket.Entities;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Bitbucket.Tests
{
	/// <summary>
	///     Tests
	/// </summary>
	public class BitbucketTests
	{
		private static readonly LogSource Log = new LogSource();
		public BitbucketTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			_bitbucketClient = BitbucketClient.Create(BitbucketTestUri);

			if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
			{
				_bitbucketClient.SetBasicAuthentication(_username, _password);
			}
		}

		private readonly string _username = Environment.GetEnvironmentVariable("bitbucket_test_username");
		private readonly string _password = Environment.GetEnvironmentVariable("bitbucket_test_password");

		// Specify the URI for the Bitbucket server
		// ReSharper disable once AssignNullToNotNullAttribute
		private static readonly Uri BitbucketTestUri = new Uri(Environment.GetEnvironmentVariable("bitbucket_test_url"));


		private readonly IBitbucketClient _bitbucketClient;

		[Fact]
		public async Task TestGetUser()
		{
			var user = await _bitbucketClient.User.GetAsync(_username);
			Assert.NotNull(user);
		}

		[Fact]
		public async Task TestGetUserAvatar()
		{
			var avatar = await _bitbucketClient.User.GetAvatarAsync<Bitmap>(_username);
			Assert.NotNull(avatar);
			Assert.True(avatar.Width > 0);
		}

		[Fact]
		public async Task TestGetProjects()
		{
			Results<Project> projects = null;
			do
			{
				projects = await _bitbucketClient.Project.GetAllAsync(projects ?? new PagingInfo {Limit = 5});
				Assert.NotNull(projects);
				Assert.True(projects.Size > 0);
				foreach (var project in projects)
				{
					Log.Info().WriteLine("{0} : {1} - {2}", project.Id, project.Description, project.Type);
				}
			} while (!projects.IsLastPage);
		}

		[Fact]
		public async Task TestGetUsers()
		{
			Results<User> users = null;
			int processedUsers = 0;
			do
			{
				users = await _bitbucketClient.User.GetAllAsync(users ?? new PagingInfo { Limit = 20 });
				Assert.NotNull(users);
				Assert.True(users.Size > 0);
				foreach (var user in users)
				{
					Log.Info().WriteLine("{0} : {1}", user.Id, user.DisplayName);
				}
				// Make sure we don't query all users, this might take forever...
				processedUsers += users.Size;
				if (processedUsers > 100)
				{
					break;
				}
			} while (!users.IsLastPage);
		}
	}
}