//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// Tag the class in an assembly that implements <see cref="IDatabaseConfig"/>
    /// so we can create an instance of it.
    /// </summary>
    [AttributeUsage( AttributeTargets.Assembly, AllowMultiple = false, Inherited = false )]
    public class DatabaseConfigAttribute : Attribute
    {
        public DatabaseConfigAttribute( Type type )
        {
            this.DatabaseConfigTypeInfo = type;
        }

        public Type DatabaseConfigTypeInfo { get; private set; }
    }

    /// <summary>
    /// This interface is used to configure a database.
    /// External DLLs will implement this interface; depending on the database type.
    /// The API will then call the interface, so the API is not tied to any database implementation.
    /// </summary>
    public interface IDatabaseConfig
    {
        void Init( string assetManagerSettingsPath );

        /// <summary>
        /// Called when <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)"/> is called.
        /// </summary>
        void OnConfiguring( DbContextOptionsBuilder optionsBuilder );

        /// <summary>
        /// Called when <see cref="DbContext.OnModelCreating(ModelBuilder)"/> is called.
        /// </summary>
        void OnModelCreating( ModelBuilder modelBuilder );
    }
}
