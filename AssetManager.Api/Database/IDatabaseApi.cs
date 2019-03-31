//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;

namespace AssetManager.Api.Database
{
    public interface IDatabaseApi
    {
        // ---------------- Functions ----------------

        /// <summary>
        /// Adds the given asset type to the database.
        /// </summary>
        void AddAssetType( AssetTypeBuilder builder );

        /// <summary>
        /// Generates an empty asset based on the given type.
        /// </summary>
        Asset GenerateEmptyAsset( string assetTypeName );

        /// <summary>
        /// Adds the given asset to the database.
        /// </summary>
        void AddAsset( Asset asset );

        /// <summary>
        /// Retrieves the names of the asset types.
        /// </summary>
        IList<string> GetAssetTypeNames();

        /// <summary>
        /// Gets information about all of the asset types.
        /// </summary>
        IList<AssetTypeInfo> GetAssetTypeInfo();
    }
}
