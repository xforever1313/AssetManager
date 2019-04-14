//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Api.Database.Tables
{
    /// <summary>
    /// This is the type of asset being use.
    /// Can also be thought of as "Asset Categories".
    /// </summary>
    /// <example>
    /// If we are keeping track of Video Games, <see cref="AssetType"/> could be
    /// something like "PC Games" and "Console Games".
    /// </example>
    internal class AssetType
    {
        // ---------------- Constructor ----------------

        public AssetType()
        {
            this.Name = string.Empty;
        }

        // ---------------- Properties ---------------

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The date in UTC time that the type was put
        /// into the database.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The date in UTC time that the type was last modified
        /// in the database.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
