//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;

namespace AssetManager.Api.Database
{
    public interface IDatabaseApi
    {
        /// <summary>
        /// A dictionary of all the database names, whose key is the database ID.
        /// </summary>
        IReadOnlyDictionary<Guid, string> DatabaseNames { get; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Adds the given asset type to the database.
        /// </summary>
        void AddAssetType( AssetTypeBuilder builder );

        /// <summary>
        /// Generates an empty asset based on the given type.
        /// </summary>
        Asset GenerateEmptyAsset( Guid databaseId, string assetTypeName );

        /// <summary>
        /// Adds the given asset to the database.
        /// </summary>
        void AddAsset( Asset asset );

        /// <summary>
        /// Gets a list of assets based on the given asset name,
        /// </summary>
        /// <returns>
        /// A list of assets that are of the type of the given asset name.
        /// </returns>
        IList<Asset> GetAssets( string assetName );

        /// <summary>
        /// Retrieves the names of the asset types.
        /// </summary>
        DatabaseQueryMultiResult<IList<string>> GetAssetTypeNames();

        /// <summary>
        /// Gets information about all of the asset types.
        /// </summary>
        IList<AssetTypeInfo> GetAssetTypeInfo();
    }
}
