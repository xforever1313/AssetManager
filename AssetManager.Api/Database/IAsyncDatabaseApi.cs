//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

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
        Task AsyncAddAssetType( AssetTypeBuilder builder );

        /// <summary>
        /// Generates an empty asset based on the given type.
        /// </summary>
        Task<Asset> AsyncGenerateEmptyAsset( string assetTypeName );

        /// <summary>
        /// Adds the given asset to the database.
        /// </summary>
        Task AsyncAddAsset( Asset asset );

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
