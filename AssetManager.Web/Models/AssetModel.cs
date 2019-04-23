﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;

namespace AssetManager.Web.Models
{
    /// <summary>
    /// This model only contains an Asset and the API.
    /// </summary>
    public class AssetModel : IModel
    {
        // ---------------- Constructor ----------------

        public AssetModel( IAssetManagerApi api, Asset asset, int assetTypeId, IAssetType assetType )
        {
            this.Api = api;
            this.Asset = asset;
            this.AssetTypeId = assetTypeId;
            this.AssetType = assetType;
        }

        // ---------------- Properties ----------------

        public IAssetManagerApi Api { get; private set; }

        public IAssetType AssetType { get; private set; }

        public Asset Asset { get; private set; }

        public int AssetTypeId { get; private set; }
    }
}
