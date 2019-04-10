//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;

namespace AssetManager.Web.Models
{
    public class AssetListModel
    {
        // ---------------- Constructor ----------------

        public AssetListModel( IAssetManagerApi api, string assetName )
        {
            this.Api = api;
            this.AssetName = assetName;
        }

        // ---------------- Properties ----------------

        public IAssetManagerApi Api { get; private set; }

        public string AssetName { get; private set; }
    }
}
