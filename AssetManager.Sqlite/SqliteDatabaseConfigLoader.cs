//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.IO;
using AssetManager.Api;
using AssetManager.Api.Database;

[assembly: DatabaseConfigLoader( typeof( AssetManager.Sqlite.SqliteDatabaseConfigLoader ) )]

namespace AssetManager.Sqlite
{
    public class SqliteDatabaseConfigLoader : IDatabaseConfigLoader
    {
        // ---------------- Constructor ----------------

        public SqliteDatabaseConfigLoader()
        {
        }

        // ---------------- Functions ----------------

        public IDatabaseConfig Load( AssetManagerSettings settings )
        {
            string filePath = Path.Combine(
                settings.AssetManagerSettingsDirectory,
                "sqlite",
                "SqliteConfig.xml"
            );

            SqliteDatabaseConfig config = new SqliteDatabaseConfig();
            config.LoadFromXml( filePath );

            return config;
        }
    }
}
