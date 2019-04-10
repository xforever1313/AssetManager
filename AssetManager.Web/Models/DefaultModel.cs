//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;

namespace AssetManager.Web.Models
{
    /// <summary>
    /// The default model most views will use.
    /// </summary>
    public class DefaultModel : IModel
    {
        // ---------------- Constructor ----------------

        public DefaultModel( IAssetManagerApi api )
        {
            this.Api = api;
        }

        // ---------------- Properties ----------------

        public IAssetManagerApi Api { get; private set; }
    }
}
