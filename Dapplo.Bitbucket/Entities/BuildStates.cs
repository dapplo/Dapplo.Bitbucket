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

using System.Diagnostics.CodeAnalysis;

namespace Dapplo.Bitbucket.Entities
{
    /// <summary>
    /// Possible states of a build
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum BuildStates
    {
        /// <summary>
        /// Build was successful
        /// </summary>
        SUCCESSFUL,
        /// <summary>
        /// Build failed
        /// </summary>
        FAILED,
        /// <summary>
        /// Build is running
        /// </summary>
        INPROGRESS
    }
}