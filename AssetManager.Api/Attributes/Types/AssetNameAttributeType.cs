﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AssetManager.Api.Attributes.Types
{
    public class AssetNameAttributeType : BaseAttributeType<AssetNameAttribute>
    {
        // ---------------- Constructor ----------------

        public AssetNameAttributeType() :
            base( AttributeTypes.AssetName )
        {
            this.Required = true;
        }

        // ---------------- Functions ----------------

        protected override IEnumerable<string> ValidateInternal()
        {
            List<string> errors = new List<string>();

            if( this.Required == false )
            {
                errors.Add( "Asset Name is always required, " + nameof( this.Required ) + " can not be false." );
            }

            return errors;
        }

        public override IEnumerable<string> TryValidateAttribute( AssetNameAttribute attr )
        {
            List<string> errors = new List<string>();

            if ( string.IsNullOrWhiteSpace( attr.Value ) )
            {
                errors.Add( "Asset Name Value can not be null, empty, or whitespace." );
            }

            return errors;
        }

        public override string SerializePossibleValues()
        {
            // There no limitations on the possible values.
            return null;
        }

        public override string SerializeDefaultValue()
        {
            return null;
        }

        public override void DeserializePossibleValues( string data )
        {
            // Ignored, there are no limitations.
        }

        public override void DeserializeDefaultValue( string data )
        {
            // Ignored, there are no default values.
        }

        public override string Serialize()
        {
            JObject root = new JObject
            {
                ["Key"] = this.Key,
                ["AttributeType"] = Convert.ToInt32( this.AttributeType ),
                ["Required"] = this.Required,
                ["DefaultValue"] = null,
                ["PossibleValues"] = this.SerializePossibleValues()
            };

            return root.ToString();
        }

        public override void Deserialize( JToken rootNode )
        {
            foreach( JProperty property in rootNode.Children<JProperty>() )
            {
                if( property.Name == "Key" )
                {
                    this.Key = property.ToObject<string>();
                }
                else if( property.Name == "AttributeType" )
                {
                    // Sanity check, make sure we are an int.
                    AttributeTypes type = (AttributeTypes)property.ToObject<long>();
                    if( type != AttributeTypes.AssetName )
                    {
                        throw new ValidationException( "Attribute type is not set to " + nameof( AttributeTypes.AssetName ) );
                    }
                }
                else if( property.Name == "Required" )
                {
                    this.Required = property.ToObject<bool>();
                }
            }
        }
    }
}
