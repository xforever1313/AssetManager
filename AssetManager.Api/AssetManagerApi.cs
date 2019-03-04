//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AssetManager.Api.Database;

namespace AssetManager.Api
{
    public class AssetManagerApi
    {
        // ---------------- Constructor ----------------

        public AssetManagerApi( AssetManagerSettings settings )
        {
            this.Init( settings );
        }

        // ---------------- Properties ----------------

        public DatabaseApi DataBase { get; private set; }

        // ---------------- Functions ----------------

        private void Init( AssetManagerSettings settings )
        {
            settings.Validate();

            // Load the assembly that contains the database information.
            Debug.WriteLine( "Loading Database Assemblies" );
            Assembly assembly = Assembly.LoadFrom( settings.DatabaseAssemblyPath );
            Debug.WriteLine( "Loading Database Assemblies...Done!" );
            DatabaseConfigAttribute attr = assembly.GetCustomAttribute<DatabaseConfigAttribute>();
            if( attr == null )
            {
                throw new InvalidOperationException(
                    "Loaded assembly " + settings.DatabaseAssemblyPath + " does not define " + nameof( DatabaseConfigAttribute )
                );
            }

            object db = Activator.CreateInstance( attr.DatabaseConfigTypeInfo );
            IDatabaseConfig databaseConfig = ( db as IDatabaseConfig );
            if( databaseConfig == null )
            {
                throw new InvalidOperationException(
                    "Class tagged as " + nameof( DatabaseConfigAttribute ) + " does not implement " + nameof( IDatabaseConfig )
                );
            }

            databaseConfig.Init( settings.AssetManagerSettingsDirectory );
            this.DataBase = new DatabaseApi( databaseConfig );
        }
    }
}
