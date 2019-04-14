//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using AssetManager.Api;

namespace AssetManager.Web.Models
{
    public class AssetListModel : IModel
    {
        // ---------------- Constructor ----------------

        public AssetListModel( IAssetManagerApi api, string assetName, IList<Asset> assets )
        {
            this.Api = api;
            this.AssetName = assetName;
            this.Assets = assets;
        }

        // ---------------- Properties ----------------

        public IAssetManagerApi Api { get; private set; }

        public string AssetName { get; private set; }

        public IList<Asset> Assets { get; private set; }
    }
}
