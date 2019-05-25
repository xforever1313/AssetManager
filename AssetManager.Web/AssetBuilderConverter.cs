//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using AssetManager.Api;
using AssetManager.Web.Models;
using Newtonsoft.Json.Linq;

namespace AssetManager.Web
{
    public class AssetBuilderConverter : JsonCreationConverter<AssetBuilderModel>
    {
        // ---------------- Constructor ----------------

        public AssetBuilderConverter() :
            base()
        {
        }

        // ---------------- Functions ----------------

        protected override AssetBuilderModel Create( Type objectType, JObject rootNode )
        {
            if ( rootNode == null )
            {
                throw new ArgumentNullException( nameof( rootNode ) );
            }

            AssetBuilderModel builder = new AssetBuilderModel();
            try
            {
                foreach ( JToken childNode in rootNode.Children() )
                {
                    if ( childNode.Path == "AttributeList" )
                    {
                        foreach ( JArray attrNode in childNode.Children<JArray>() )
                        {
                            foreach ( JObject attrProperties in attrNode )
                            {
                                string keyStr = null;
                                AttributeTypes? attrType = null;
                                string valueStr = null;
                                foreach ( JProperty attrProperty in attrProperties.Children<JProperty>() )
                                {
                                    if ( attrProperty.Name == "Key" )
                                    {
                                        keyStr = attrProperty.ToObject<string>();
                                    }
                                    else if ( attrProperty.Name == "AttributeType" )
                                    {
                                        attrType = (AttributeTypes)attrProperty.ToObject<long>();
                                    }
                                    else if ( attrProperty.Name == "Value" )
                                    {
                                        valueStr = attrProperty.ToObject<string>();
                                    }
                                }

                                if ( string.IsNullOrEmpty( keyStr ) || string.IsNullOrEmpty( valueStr ) || ( attrType == null ) )
                                {
                                    throw new InvalidOperationException( "Can not have null value in JSON" );
                                }

                                IAttribute attribute = AttributeFactory.CreateAttribute( attrType.Value );
                                attribute.Deserialize( valueStr );
                                KeyValuePair<string, IAttribute> kvPair = new KeyValuePair<string, IAttribute>( keyStr, attribute );
                                builder.Attributes.Add( kvPair );
                            }
                        }
                    }
                }

                builder.Success = true;
                builder.ErrorMessage = string.Empty;
            }
            catch ( Exception e )
            {
                builder.Success = false;
                builder.ErrorMessage = e.Message;
            }
            return builder;
        }
    }
}
