//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;
using AssetManager.Api.Attributes.Types;
using Newtonsoft.Json.Linq;
using SethCS.Exceptions;

namespace AssetManager.Api
{
    /// <summary>
    /// This class aides in building an Asset Type.
    /// </summary>
    public class AssetTypeBuilder
    {
        // ---------------- Fields ----------------

        internal const string UnknownType = "Unknown Asset";

        // ---------------- Constructor ----------------

        public AssetTypeBuilder() :
            this( UnknownType, Guid.Empty )
        {
        }

        public AssetTypeBuilder( string assetName, Guid databaseId )
        {
            this.Name = assetName;
            this.AttributeTypes = new List<IAttributeType>();
            this.DatabaseId = databaseId;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The name of the specific asset instance.
        /// </summary>
        public string Name { get; set; }

        public IList<IAttributeType> AttributeTypes { get; protected set; }

        /// <summary>
        /// The database ID to add this asset type to.
        /// </summary>
        public Guid DatabaseId { get; set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Validates both this class and all the attributes.
        /// Throws a <see cref="ValidationException"/>
        /// if this object is not valid.
        /// </summary>
        public void Validate()
        {
            bool success = true;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine( "Errors when validating the Asset Type Builder:" );

            if( string.IsNullOrWhiteSpace( this.Name ) )
            {
                success = false;
                builder.AppendLine( "- Name can not be null, empty, or whitespace." );
            }

            if ( this.DatabaseId.Equals( Guid.Empty ) )
            {
                success = false;
                builder.AppendLine( "- Database ID can not be empty." );
            }

            foreach( IAttributeType attribute in AttributeTypes )
            {
                if( attribute.TryValidate( out string attrErrors ) == false )
                {
                    success = false;
                    builder.AppendLine( attrErrors );
                }
            }

            if( success == false )
            {
                throw new ValidationException( builder.ToString() );
            }
        }

        public void Deserialize( JToken rootNode )
        {
            foreach( JToken childNode in rootNode.Children() )
            {
                if ( childNode.Path == "AssetTypeName" )
                {
                    string name = childNode.ToObject<string>();
                    if ( string.IsNullOrWhiteSpace( name ) == false )
                    {
                        this.Name = name;
                    }
                }
                else if ( childNode.Path == "AttributeList" )
                {
                    // This is the array of attributes.
                    foreach ( JArray attrNode in childNode.Children<JArray>() )
                    {
                        foreach ( JObject attrProperties in attrNode )
                        {
                            AttributeTypes? attrType = null;
                            foreach ( JProperty attrValue in attrProperties.Children<JProperty>() )
                            {
                                if ( attrValue.Name == "AttributeType" )
                                {
                                    attrType = (AttributeTypes)attrValue.ToObject<long>();
                                }
                            }

                            if ( attrType == null )
                            {
                                throw new InvalidOperationException( "Attribute Type can not be null." );
                            }

                            IAttributeType attributeType = AttributeTypeFactory.CreateAttributeType( attrType.Value );
                            attributeType.Deserialize( attrProperties );
                            this.AttributeTypes.Add( attributeType );
                        }

                        // There should only be one array.  Break.
                        break;
                    }
                }
                else if ( childNode.Path == "DatabaseId" )
                {
                    string guidStr = childNode.ToObject<string>();
                    Guid guid = Guid.Parse( guidStr );
                    this.DatabaseId = guid;
                }
            }
        }
    }
}
