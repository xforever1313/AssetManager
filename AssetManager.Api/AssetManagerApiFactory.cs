//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssetManager.Api.Database;
using SethCS.Exceptions;

namespace AssetManager.Api
{
    public static class AssetManagerApiFactory
    {
        // ---------------- Constructor ----------------

        static AssetManagerApiFactory()
        {
            DefaultSettingsDirectory = Path.Combine(
                Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ),
                "AssetManager"
            );

            ConfigFileName = "AssetManagerConfig.xml";

            DefaultConfigFile = Path.Combine( DefaultSettingsDirectory, ConfigFileName );
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Default directory where the AssetManager settings are located.
        /// </summary>
        public static string DefaultSettingsDirectory { get; private set; }

        /// <summary>
        /// The config file name for AssetManager (NOT the path, just the name!).
        /// </summary>
        public static string ConfigFileName { get; private set; }

        /// <summary>
        /// Full path to the default configuration file location.
        /// </summary>
        public static string DefaultConfigFile { get; private set; }

        // ---------------- Functions ----------------

        public static IAssetManagerApi CreateApiFromDefaultConfigFile()
        {
            return CreateApiFromConfig( DefaultSettingsDirectory );
        }

        public static IAssetManagerApi CreateApiFromConfig( string configDirectory )
        {
            AssetManagerSettings settings = new AssetManagerSettings
            {
                AssetManagerSettingsDirectory = configDirectory
            };

            settings.LoadFromXml( Path.Combine( configDirectory, ConfigFileName ) );

            return CreateApiFromSettings( settings );
        }

        public static IAssetManagerApi CreateApiFromSettings( AssetManagerSettings settings )
        {
            settings.Validate();

            IList<IDatabaseConfig> dbConfigs = LoadDataBase( settings );

            return new AssetManagerApi( dbConfigs );
        }

        private static IList<IDatabaseConfig> LoadDataBase( AssetManagerSettings settings )
        {
            // Load the assembly that contains the database information.
            Assembly assembly = Assembly.LoadFrom( settings.DatabaseAssemblyPath );

            DatabaseConfigLoaderAttribute attr = assembly.GetCustomAttribute<DatabaseConfigLoaderAttribute>();
            if( attr == null )
            {
                throw new InvalidOperationException(
                    "Loaded assembly " + settings.DatabaseAssemblyPath + " does not define " + nameof( DatabaseConfigLoaderAttribute )
                );
            }

            object dbLoader = Activator.CreateInstance( attr.DatabaseConfigLoaderType );
            IDatabaseConfigLoader loader = dbLoader as IDatabaseConfigLoader;
            if( loader == null )
            {
                throw new InvalidOperationException(
                    "Class tagged as " + nameof( DatabaseConfigLoaderAttribute ) + " does not implement " + nameof( IDatabaseConfigLoader )
                );
            }

            IList<IDatabaseConfig> databaseConfigs = loader.Load( settings );
            foreach ( IDatabaseConfig databaseConfig in databaseConfigs )
            {
                databaseConfig.Validate();
            }

            foreach ( IDatabaseConfig databaseConfig in databaseConfigs )
            {
                IEnumerable<Guid> ids = databaseConfigs.Select( d => d.DatabaseId ).Where( d => d.Equals( databaseConfig.DatabaseId ) );
                if ( ids.Count() != 1 )
                {
                    throw new ValidationException( "More than one database has ID " + databaseConfig.DatabaseId );
                }
            }

            return databaseConfigs;
        }
    }
}
