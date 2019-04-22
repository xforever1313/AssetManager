//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManager.Api.Database.Tables;

namespace AssetManager.Api.Database
{
    public class AsyncDatabaseApi : IAsyncDatabaseApi
    {
        // ---------------- Fields ----------------

        private readonly IDatabaseApi databaseApi;

        // ---------------- Constructor ----------------

        public AsyncDatabaseApi( IDatabaseApi databaseApi )
        {
            this.databaseApi = databaseApi;
        }

        // ---------------- Functions ----------------

        public Task<int> AsyncAddAssetType( AssetTypeBuilder builder )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAssetType( builder ) );
        }

        public Task<Asset> AsyncGenerateEmptyAsset( Guid databaseId, int assetTypeId )
        {
            return Task.Factory.StartNew( () => this.databaseApi.GenerateEmptyAsset( databaseId, assetTypeId ) );
        }

        public Task<int> AsyncAddAsset( Asset asset )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAsset( asset ) );
        }

        public Task<AssetListInfo> GetAssets( Guid databaseId, int assetTypeId )
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetsOfType( databaseId, assetTypeId ) );
        }

        public Task<DatabaseQueryMultiResult<IList<AssetType>>> AsyncGetAssetTypeNames()
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetTypeNames() );
        }

        public Task<DatabaseQueryMultiResult<IList<AssetTypeInfo>>> AsyncGetAssetTypeInfo()
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetTypeInfo() );
        }
    }
}
