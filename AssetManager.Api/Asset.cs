//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AssetManager.Api
{
    /// <summary>
    /// This is a specific instance of an asset.
    /// </summary>
    public class Asset
    {
        // ---------------- Fields ----------------

        private string type;
        private string name;
        private readonly Dictionary<string, string> kvAttributes;

        // ---------------- Constructor ----------------

        public Asset()
        {
            this.type = AssetTypeBuilder.UnknownType;
            this.Name = "Untitled Asset";
            this.kvAttributes = new Dictionary<string, string>();

            this.KeyValueAttributes = new ReadOnlyDictionary<string, string>( this.kvAttributes );
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The type of asset this is.
        /// </summary>
        public string AssetType
        {
            get
            {
                return this.type;
            }
            set
            {
                if( string.IsNullOrWhiteSpace( value ) )
                {
                    throw new ArgumentException( nameof( AssetType ) + " can not be null, empty, or whitespace." );
                }
                this.type = value;
            }
        }

        /// <summary>
        /// The name of the specific asset instance.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if( string.IsNullOrWhiteSpace( value ) )
                {
                    throw new ArgumentException( nameof( Name ) + " can not be null, empty, or whitespace." );
                }
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the given key in the Key-Value attribute's value.
        /// </summary>
        /// <exception cref="KeyNotFoundException">If the key does not exist.</exception>
        public string this[string key]
        {
            get
            {
                this.KeyCheck( key );
                return this.KeyValueAttributes[key];
            }
            set
            {
                //this.KeyCheck( key ); // For now...
                this.kvAttributes[key] = value;
            }
        }

        /// <summary>
        /// Key-Value attributes associated with this asset.
        /// </summary>
        public IReadOnlyDictionary<string, string> KeyValueAttributes { get; private set; }

        // ---------------- Functions ----------------

        private void KeyCheck( string key )
        {
            if( this.KeyValueAttributes.ContainsKey( key ) == false )
            {
                throw new KeyNotFoundException(
                    "Could not find key '" + key + "' in asset " + this.Name + "'s Key-Value attributes."
                );
            }
        }
    }
}
