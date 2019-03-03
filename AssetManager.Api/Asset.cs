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
        private readonly Dictionary<string, IAttribute> kvAttributes;

        // ---------------- Constructor ----------------

        internal Asset()
        {
            this.type = AssetTypeBuilder.UnknownType;
            this.Name = "Untitled Asset";
            this.kvAttributes = new Dictionary<string, IAttribute>();

            this.KeyValueAttributes = new ReadOnlyDictionary<string, IAttribute>( this.kvAttributes );
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
            internal set
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
        internal IAttribute this[string key]
        {
            get
            {
                this.KeyCheck( key );
                return this.KeyValueAttributes[key];
            }
            set
            {
                this.KeyCheck( key );

                if( this.kvAttributes[key].AttributeType != value.AttributeType )
                {
                    throw new InvalidOperationException(
                        "Attribute Types not compatible, need to pass in the correct attribute type for key '" + key + "'"
                    );
                }

                this.kvAttributes[key] = value;
            }
        }

        /// <summary>
        /// Key-Value attributes associated with this asset.
        /// </summary>
        internal IReadOnlyDictionary<string, IAttribute> KeyValueAttributes { get; private set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Creates a new instance of the attribute inside the given key
        /// so it can be modified without affecting this class's internal list.
        /// Once modified, pass it into <see cref="SetAttribute(string, IAttribute)"/>
        /// to modify the value.
        /// </summary>
        public TAttr CloneAttributeAsType<TAttr>( string key ) where TAttr : IAttribute
        {
            this.KeyCheck( key );

            TAttr attr = AttributeFactory.CreateAttribute<TAttr>();
            if( attr.AttributeType != this.kvAttributes[key].AttributeType )
            {
                throw new InvalidOperationException(
                    "Attribute Types not compatible, need to pass in the correct attribute type for key '" + key + "'"
                );
            }

            attr.Deserialize( this.kvAttributes[key].Serialize() );

            return attr;
        }

        public void SetAttribute( string key, IAttribute value )
        {
            this[key] = value;
        }

        /// <summary>
        /// Adds a new key that is set to an empty instance.
        /// Internal since we only do this when creating a new Asset that
        /// consumers of the API will use.
        /// </summary>
        internal void AddEmptyKeyValue( string newKey, AttributeTypes attributeType )
        {
            this.kvAttributes[newKey] = AttributeFactory.CreateAttribute( attributeType );
        }

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
