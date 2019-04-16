//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This class contains basic information about an <see cref="AssetType"/>
    /// </summary>
    public class AssetTypeInfo
    {
        // ---------------- Constructor ----------------

        public AssetTypeInfo( string name, Guid databaseId, int numberOfAssets, string databaseName )
        {
            this.Name = name;
            this.DatabaseId = databaseId;
            this.NumberOfAssets = numberOfAssets;
            this.DatabaseName = databaseName;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The name of the asset type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The ID of the database that holds the asset type.
        /// </summary>
        public Guid DatabaseId { get; private set; }

        /// <summary>
        /// The database name that holds the asset type.
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// The number of assets inside of this asset type.
        /// </summary>
        public int NumberOfAssets { get; private set; }
    }
}
