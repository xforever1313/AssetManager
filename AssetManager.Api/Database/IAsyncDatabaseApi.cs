//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This is the same interface as <see cref="IDatabaseApi"/>,
    /// but all functions return tasks.
    /// </summary>
    public interface IAsyncDatabaseApi
    {
        // ---------------- Functions ----------------

        /// <summary>
        /// Adds the given asset type to the database.
        /// </summary>
        Task AsyncAddAssetType( Guid databaseId, AssetTypeBuilder builder );

        /// <summary>
        /// Generates an empty asset based on the given type.
        /// </summary>
        Task<Asset> AsyncGenerateEmptyAsset( Guid databaseId, string assetTypeName );

        /// <summary>
        /// Adds the given asset to the database.
        /// </summary>
        Task AsyncAddAsset( Asset asset );

        /// <summary>
        /// Gets a list of assets based on the given asset name,
        /// </summary>
        /// <returns>
        /// A list of assets that are of the type of the given asset name.
        /// </returns>
        Task<IList<Asset>> GetAssets( string assetName );

        /// <summary>
        /// Retrieves the names of the asset types.
        /// </summary>
        Task<IList<string>> AsyncGetAssetTypeNames();

        /// <summary>
        /// Gets information about all of the asset types.
        /// </summary>
        Task<IList<AssetTypeInfo>> AsyncGetAssetTypeInfo();
    }
}
