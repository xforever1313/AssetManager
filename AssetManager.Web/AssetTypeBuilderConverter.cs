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

        protected override AssetTypeBuilderModel Create( Type objectType, JObject jObject )
        {
            if( jObject == null )
            {
                throw new ArgumentNullException( nameof( jObject ) );
            }

            JToken rootNode = jObject.First;
            if( rootNode == null )
            {
                throw new ArgumentException( "Malformed JSON.  Can not find root node" );
            }

            AssetTypeBuilderModel builder = new AssetTypeBuilderModel( "Test" );

            foreach( JToken childNode in rootNode.Children() )
            {
                if( childNode.Path == "AttributeList" )
                {
                    foreach( JToken attrNode in childNode.Children() )
                    {
                        string key = null;
                        AttributeTypes? attrType = null;

                        foreach( JProperty attrValue in attrNode.Children<JProperty>() )
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
                                type = new IntegerAttributeType
                                {
                                    Key = key
                                };
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
                }
            }

            return builder;
        }
    }
}
