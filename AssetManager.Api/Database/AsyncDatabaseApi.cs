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

        public Task AsyncAddAssetType( AssetTypeBuilder builder )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAssetType( builder ) );
        }

        public Task<Asset> AsyncGenerateEmptyAsset( string assetTypeName )
        {
            return Task.Factory.StartNew( () => this.databaseApi.GenerateEmptyAsset( assetTypeName ) );
        }

        public Task AsyncAddAsset( Asset asset )
        {
            return Task.Factory.StartNew( () => this.databaseApi.AddAsset( asset ) );
        }

        public Task<IList<string>> AsyncGetAssetTypeNames()
        {
            return Task.Factory.StartNew( () => this.databaseApi.GetAssetTypeNames() );
        }
    }
}
