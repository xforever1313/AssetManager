//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using AssetManager.Api.Database;

namespace AssetManager.Api
{
    public class AssetManagerApi : IAssetManagerApi
    {
        // ---------------- Constructor ----------------

        /// <summary>
        /// Creates an instance of the top-level API.
        /// </summary>
        /// <param name="databaseConfigs">
        /// The database config(s) to use.
        /// Assumption is it was already inited.
        /// </param>
        public AssetManagerApi( IList<IDatabaseConfig> databaseConfigs )
        {
            this.DataBase = new DatabaseApi( databaseConfigs );
            this.AsyncDataBase = new AsyncDatabaseApi( this.DataBase );

            this.ApiVersion = typeof( AssetManagerApi ).Assembly.GetName().Version.ToString();
        }

        // ---------------- Properties ----------------

        public string ApiVersion { get; private set; }

        public IDatabaseApi DataBase { get; private set; }

        public IAsyncDatabaseApi AsyncDataBase { get; private set; }

        // ---------------- Functions ----------------
    }
}
