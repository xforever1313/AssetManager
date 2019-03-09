//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using AssetManager.Api;

namespace AssetManager.Web
{
    /// <summary>
    /// Classes that extend (or at least pretend to) the API.
    /// </summary>
    public static class ApiExtensions
    {
        public static IReadOnlyList<CreditsInfo> GetCredits( this IAssetManagerApi api )
        {
            return CreditsInfo.AllCredits;
        }
    }
}
