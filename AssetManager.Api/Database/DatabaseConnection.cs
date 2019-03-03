//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    internal class DatabaseConnection : DbContext
    {
        // ---------------- Fields ----------------

        // ---------------- Constructor ----------------

        public DatabaseConnection()
        {
            this.Database.EnsureCreated();
        }

        // ---------------- Properties ----------------

        public DbSet<AssetInstance> AssetInstances { get; set; }

        public DbSet<AssetInstanceKeyValueAttributeValues> AssetInstanceKeyValueAttributeValues { get; set; }

        public DbSet<AssetType> AssetTypes { get; set; }

        public DbSet<AssetTypeKeyValueAttributesMap> AssetTypeKeyValueAttributesMaps { get; set; }

        public DbSet<KeyValueAttributeType> KeyValueAttributeTypes { get; set; }

        // ---------------- Functions ----------------

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlite( @"Data Source=C:\Users\xfore\Downloads\assetmanager.db" );
        }
    }
}
