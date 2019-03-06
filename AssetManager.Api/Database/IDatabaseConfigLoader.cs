//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// Tag the class in an assembly that implements <see cref="IDatabaseConfigLoader"/>
    /// so we can create an instance of it.
    /// </summary>
    [AttributeUsage( AttributeTargets.Assembly, AllowMultiple = false, Inherited = false )]
    public class DatabaseConfigLoaderAttribute : Attribute
    {
        public DatabaseConfigLoaderAttribute( Type type )
        {
            this.DatabaseConfigLoaderType = type;
        }

        public Type DatabaseConfigLoaderType { get; private set; }
    }

    /// <summary>
    /// This interface loads a database configuration
    /// </summary>
    public interface IDatabaseConfigLoader
    {
        // ---------------- Functions ----------------

        /// <summary>
        /// Reads in the database configuration
        /// based on the passed in settings object.
        /// </summary>
        IDatabaseConfig Load( AssetManagerSettings settings );
    }
}
