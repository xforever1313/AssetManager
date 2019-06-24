//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AssetManager.Api
{
    /// <summary>
    /// This is a specific instance of an asset.
    /// </summary>
    public class Asset
    {
        // ---------------- Fields ----------------

        private string type;
        private readonly Dictionary<string, IAttribute> attributes;

        // ---------------- Constructor ----------------

        internal Asset( Guid databaseId )
        {
            this.DatabaseId = databaseId;

            this.type = AssetTypeBuilder.UnknownType;
            this.attributes = new Dictionary<string, IAttribute>();

            this.Attributes = new ReadOnlyDictionary<string, IAttribute>( this.attributes );
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The database this asset is associated with.
        /// </summary>
        public Guid DatabaseId { get; private set; }

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
        /// This is set when an attribute of type <seealso cref="AttributeTypes.AssetName"/> is passed into
        /// <seealso cref="SetAttribute(string, IAttribute)"/>.
        /// 
        /// This will return the value of the FIRST attribute of type <seealso cref="AttributeTypes.AssetName"/> in this asset.
        /// Or "Untitled Asset"
        /// </summary>
        public string Name
        {
            get
            {
                IAttribute attr = this.attributes.Values.FirstOrDefault( a => a.AttributeType == AttributeTypes.AssetName );
                if ( attr == null )
                {
                    return "Untitled Asset";
                }
                else
                {
                    return attr.ToString();
                }
            }
        }

        /// <summary>
        /// Key-Value attributes associated with this asset.
        /// </summary>
        internal IReadOnlyDictionary<string, IAttribute> Attributes { get; private set; }

        /// <summary>
        /// All of the key names.
        /// </summary>
        public IEnumerable<string> Keys { get { return this.Attributes.Keys; } }

        // ---------------- Functions ----------------

        /// <summary>
        /// Does the asset contain the given key?
        /// </summary>
        public bool ContainsKey( string key )
        {
            return this.attributes.ContainsKey( key );
        }

        public AttributeTypes GetAttributeTypeOfKey( string key )
        {
            this.KeyCheck( key );
            return this.attributes[key].AttributeType;
        }

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
            if( attr.AttributeType != this.attributes[key].AttributeType )
            {
                throw new InvalidOperationException(
                    "Attribute Types not compatible, need to pass in the correct attribute type for key '" + key + "'"
                );
            }

            attr.Deserialize( this.attributes[key].Serialize() );

            return attr;
        }

        public void SetAttribute( string key, IAttribute value )
        {
            this.KeyCheck( key );

            if ( this.attributes[key].AttributeType != value.AttributeType )
            {
                throw new InvalidOperationException(
                    "Attribute Types not compatible, need to pass in the correct attribute type for key '" + key + "'"
                );
            }

            this.attributes[key] = value;
        }

        /// <summary>
        /// Adds a new key that is set to an empty instance.
        /// Internal since we only do this when creating a new Asset that
        /// consumers of the API will use.
        /// </summary>
        internal void AddEmptyAttribute( string newKey, AttributeTypes attributeType )
        {
            this.attributes[newKey] = AttributeFactory.CreateAttribute( attributeType );
        }

        private void KeyCheck( string key )
        {
            if( this.Attributes.ContainsKey( key ) == false )
            {
                throw new KeyNotFoundException(
                    "Could not find key '" + key + "' in asset " + this.Name + "'s Key-Value attributes."
                );
            }
        }
    }
}
