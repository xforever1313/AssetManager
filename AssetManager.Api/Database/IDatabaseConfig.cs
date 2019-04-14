//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This interface is used to configure a database.
    /// External DLLs will implement this interface; depending on the database type.
    /// The API will then call the interface, so the API is not tied to any database implementation.
    /// </summary>
    public interface IDatabaseConfig
    {
        /// <summary>
        /// The name of the database.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Ensures the database configuration is in a good state.
        /// </summary>
        void Validate();

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
