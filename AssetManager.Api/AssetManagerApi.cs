//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api.Database;

namespace AssetManager.Api
{
    public class AssetManagerApi
    {
        // ---------------- Constructor ----------------

        /// <summary>
        /// Creates an instance of the top-level API.
        /// </summary>
        /// <param name="databaseConfig">
        /// The database config to use.
        /// Assumption is it was already inited.
        /// </param>
        public AssetManagerApi( IDatabaseConfig databaseConfig )
        {
            this.DataBase = new DatabaseApi( databaseConfig );
        }

        // ---------------- Properties ----------------

        public DatabaseApi DataBase { get; private set; }

        // ---------------- Functions ----------------
    }
}
