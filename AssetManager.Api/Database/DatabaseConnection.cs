//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    internal class DatabaseConnection : DbContext
    {
        // ---------------- Fields ----------------

        private readonly IDatabaseConfig databaseConfig;

        // ---------------- Constructor ----------------

        public DatabaseConnection( IDatabaseConfig databaseConfig )
        {
            this.databaseConfig = databaseConfig;

            this.Database.EnsureCreated();
        }

        // ---------------- Properties ----------------

        public DbSet<AssetInstance> AssetInstances { get; set; }

        public DbSet<AssetInstanceAttributeValues> AssetInstanceAttributeValues { get; set; }

        public DbSet<AssetType> AssetTypes { get; set; }

        public DbSet<AttributeProperties> AttributeProperties { get; set; }

        public DbSet<AssetTypeAttributesMap> AssetTypeAttributesMaps { get; set; }

        public DbSet<AttributeKeys> AttributeKeys { get; set; }

        // ---------------- Functions ----------------

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            this.databaseConfig.OnConfiguring( optionsBuilder );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            this.databaseConfig.OnModelCreating( modelBuilder );
        }
    }
}
