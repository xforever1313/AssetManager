//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using AssetManager.Api;
using AssetManager.Api.Attributes.Types;
using AssetManager.Web.Models;
using Newtonsoft.Json.Linq;

namespace AssetManager.Web
{
    public class AssetTypeBuilderConverter : JsonCreationConverter<AssetTypeBuilderModel>
    {
        // ---------------- Constructor ----------------

        public AssetTypeBuilderConverter() :
            base()
        {
        }

        // ---------------- Functions ----------------

        protected override AssetTypeBuilderModel Create( Type objectType, JObject rootNode )
        {
            if( rootNode == null )
            {
                throw new ArgumentNullException( nameof( rootNode ) );
            }

            AssetTypeBuilderModel builder = new AssetTypeBuilderModel( "Test" );

            foreach( JToken childNode in rootNode.Children() )
            {
                if( childNode.Path == "AssetTypeName" )
                {
                    string name = childNode.ToObject<string>();
                    if( string.IsNullOrWhiteSpace( name ) == false )
                    {
                        builder.Name = name;
                    }
                }
                else if( childNode.Path == "AttributeList" )
                {
                    // This is the array of attributes.
                    foreach( JArray attrNode in childNode.Children<JArray>() )
                    {
                        foreach( JObject attrProperties in attrNode )
                        {
                            string key = null;
                            AttributeTypes? attrType = null;

                            foreach( JProperty attrValue in attrProperties.Children<JProperty>() )
                            {
                                if( attrValue.Name == "Key" )
                                {
                                    key = attrNode.Value<string>( attrValue.Name );
                                }
                                else if( attrValue.Name == "AttributeType" )
                                {
                                    attrType = (AttributeTypes)attrNode.Value<long>( attrValue.Name );
                                }
                            }
                            if( ( key == null ) || ( attrType == null ) )
                            {
                                throw new InvalidOperationException( "Key or Attribute Type can not be null." );
                            }

                            IAttributeType type;
                            switch( attrType.Value )
                            {
                                case AttributeTypes.StringAttribute:
                                    type = new StringAttributeType
                                    {
                                        Key = key
                                    };
                                    break;

                                case AttributeTypes.Integer:
                                    IntegerAttributeType intType = new IntegerAttributeType
                                    {
                                        Key = key
                                    };

                                    if( attrNode["Properties"] != null )
                                    {
                                        //intType.Deserialize( attrNode["Properties"] );
                                    }

                                    type = intType;
                                    break;

                                case AttributeTypes.AssetName:
                                    type = new AssetNameAttributeType
                                    {
                                        Key = key
                                    };
                                    break;

                                default:
                                    throw new InvalidOperationException( "Invalid attribute type: " + attrType.Value );
                            }

                            builder.AttributeTypes.Add( type );
                        }

                        // There should only be one array.  Break.
                        break;
                    }
                }
            }

            return builder;
        }
    }
}
