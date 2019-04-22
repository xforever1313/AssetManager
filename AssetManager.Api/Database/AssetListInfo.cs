//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// Information that is returned from the database
    /// when querying an asset list of a specfied type.
    /// </summary>
    public class AssetListInfo
    {
        // ---------------- Constructor ----------------

        public AssetListInfo( Guid databaseId, IList<Asset> assetList, int assetTypeId, string assetTypeName )
        {
            this.DatabaseId = databaseId;
            this.AssetList = assetList;
            this.AssetTypeId = assetTypeId;
            this.AssetTypeName = assetTypeName;
        }

        // ---------------- Properties ----------------

        public Guid DatabaseId { get; private set; }

        /// <summary>
        /// A list of the assets that fall under the specified type.
        /// </summary>
        public IList<Asset> AssetList { get; private set; }

        /// <summary>
        /// The ID of the asset type these assets fall under.
        /// </summary>
        public int AssetTypeId { get; private set; }

        /// <summary>
        /// The name of the asset type these assets fall under.
        /// </summary>
        public string AssetTypeName { get; private set; }
    }
}
