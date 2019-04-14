//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using AssetManager.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Sqlite
{
    public class SqliteDatabaseConfig : IDatabaseConfig
    {
        // ---------------- Constructor ----------------

        public SqliteDatabaseConfig()
        {
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The name in this case is the filename of the sqlite file.
        /// </summary>
        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension( this.DatabaseLocation );
            }
        }

        public string DatabaseLocation { get; set; }

        // ---------------- Functions ----------------

        public void Validate()
        {
            if( string.IsNullOrWhiteSpace( this.DatabaseLocation ) )
            {
                throw new InvalidOperationException(
                    nameof( this.DatabaseLocation ) + " can not be null, empty, or whitespace."
                );
            }
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
