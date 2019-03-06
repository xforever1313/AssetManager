﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using System.Reflection;
using AssetManager.Api.Database;

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

        public static AssetManagerApi CreateApiFromDefaultConfigFile()
        {
            return CreateApiFromConfig( DefaultSettingsDirectory );
        }

        public static AssetManagerApi CreateApiFromConfig( string configDirectory )
        {
            AssetManagerSettings settings = new AssetManagerSettings
            {
                AssetManagerSettingsDirectory = configDirectory
            };

            settings.LoadFromXml( Path.Combine( configDirectory, ConfigFileName ) );

            return CreateApiFromSettings( settings );
        }

        public static AssetManagerApi CreateApiFromSettings( AssetManagerSettings settings )
        {
            settings.Validate();

            IDatabaseConfig config = LoadDataBase( settings );

            return new AssetManagerApi( config );
        }

        private static IDatabaseConfig LoadDataBase( AssetManagerSettings settings )
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

            IDatabaseConfig databaseConfig = loader.Load( settings );
            databaseConfig.Validate();

            return databaseConfig;
        }
    }
}
