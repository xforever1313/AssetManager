//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using System.IO;
using System.Xml;
using AssetManager.Api;
using AssetManager.Api.Database;

[assembly: DatabaseConfigLoader( typeof( AssetManager.Sqlite.SqliteDatabaseConfigLoader ) )]

namespace AssetManager.Sqlite
{
    public class SqliteDatabaseConfigLoader : IDatabaseConfigLoader
    {
        // ---------------- Fields ----------------

        internal const string SettingsXmlElementName = "AssetManagerSqliteConfig";

        // ---------------- Constructor ----------------

        public SqliteDatabaseConfigLoader()
        {
        }

        // ---------------- Functions ----------------

        public IList<IDatabaseConfig> Load( AssetManagerSettings settings )
        {
            string filePath = Path.Combine(
                settings.AssetManagerSettingsDirectory,
                "sqlite",
                "SqliteConfig.xml"
            );

            XmlDocument doc = new XmlDocument();
            doc.Load( filePath );

            XmlNode rootNode = doc.DocumentElement;
            if ( rootNode.Name != SettingsXmlElementName )
            {
                throw new XmlException(
                    "Root XML node should be named \"" + SettingsXmlElementName + "\".  Got: " + rootNode.Name
                );
            }

            List<IDatabaseConfig> databaseConfigs = new List<IDatabaseConfig>();

            foreach ( XmlNode childNode in rootNode.ChildNodes )
            {
                switch ( childNode.Name.ToLower() )
                {
                    case XmlLoader.DatabaseSettingsNodeName:
                        SqliteDatabaseConfig config = new SqliteDatabaseConfig();
                        config.LoadFromXml( childNode );
                        databaseConfigs.Add( config );
                        break;
                }
            }
            return databaseConfigs;
        }
    }
}
