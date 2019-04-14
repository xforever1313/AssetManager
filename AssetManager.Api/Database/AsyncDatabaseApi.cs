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

        public Task AsyncAddAssetType( Guid databaseId, AssetTypeBuilder builder )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAssetType( databaseId, builder ) );
        }

        public Task<Asset> AsyncGenerateEmptyAsset( Guid databaseId, string assetTypeName )
        {
            return Task.Factory.StartNew( () => this.databaseApi.GenerateEmptyAsset( databaseId, assetTypeName ) );
        }

        public Task AsyncAddAsset( Asset asset )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAsset( asset ) );
        }

        public Task<IList<Asset>> GetAssets( string assetName )
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssets( assetName ) );
        }

        public Task<IList<string>> AsyncGetAssetTypeNames()
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetTypeNames() );
        }

        public Task<IList<AssetTypeInfo>> AsyncGetAssetTypeInfo()
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetTypeInfo() );
        }
    }
}
