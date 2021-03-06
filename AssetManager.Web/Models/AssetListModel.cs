﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using AssetManager.Api;
using AssetManager.Api.Database;

namespace AssetManager.Web.Models
{
    public class AssetListModel : IModel
    {
        // ---------------- Constructor ----------------

        public AssetListModel( IAssetManagerApi api, AssetListInfo listInfo )
        {
            this.DatabaseId = listInfo.DatabaseId;
            this.Api = api;
            this.AssetName = listInfo.AssetTypeName;
            this.AssetTypeID = listInfo.AssetTypeId;
            this.Assets = listInfo.AssetList;
        }

        // ---------------- Properties ----------------

        public Guid DatabaseId { get; private set; }

        public IAssetManagerApi Api { get; private set; }

        public string AssetName { get; private set; }

        public int AssetTypeID { get; private set; }

        public IReadOnlyDictionary<int, Asset> Assets { get; private set; }
    }
}
