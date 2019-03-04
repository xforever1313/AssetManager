//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AssetManager.Api.Database;
using Microsoft.EntityFrameworkCore;

[assembly: DatabaseConfig( typeof( AssetManager.Sqlite.SqliteDatabaseConfig ) )]

namespace AssetManager.Sqlite
{
    public class SqliteDatabaseConfig : IDatabaseConfig
    {
        // ---------------- Constructor ----------------

        public SqliteDatabaseConfig()
        {
        }

        // ---------------- Properties ----------------

        public string DatabaseLocation { get; private set; }

        // ---------------- Functions ----------------

        public void Init( string assetManagerSettingsPath )
        {
            // For now...
            this.DatabaseLocation = @"C:\Users\xfore\Downloads\assetmanager.db";
        }

        public void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlite( "Data Source=" + this.DatabaseLocation );
        }

        public void OnModelCreating( ModelBuilder modelBuilder )
        {
            // Nothing to do.
        }
    }
}
