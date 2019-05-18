//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace AssetManager.Api.Attributes.Types
{
    public class StringAttributeType : BaseAttributeType
    {
        // ---------------- Constructor ----------------

        public StringAttributeType() :
            base( AttributeTypes.StringAttribute )
        {
        }

        // ---------------- Properties ----------------

        public string DefaultValue { get; set; }

        // ---------------- Functions ----------------

        protected override bool ValidateInternal( out string errors )
        {
            // Nothing to validate; Default value is okay to be null.
            errors = string.Empty;
            return true;
        }

        public override string SerializePossibleValues()
        {
            return null;
        }

        public override string SerializeDefaultValue()
        {
            return this.DefaultValue;
        }

        public override void DeserializePossibleValues( string data )
        {
            // Nothing to do.
        }

        public override void DeserializeDefaultValue( string data )
        {
            this.DefaultValue = data;
        }

        public override string Serialize()
        {
            JObject root = new JObject
            {
                ["Key"] = this.Key,
                ["AttributeType"] = Convert.ToInt32( this.AttributeType ),
                ["Required"] = this.Required,
                ["DefaultValue"] = this.DefaultValue,
                ["PossibleValues"] = SerializePossibleValues()
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
                    if( type != AttributeTypes.StringAttribute )
                    {
                        throw new ValidationException( "Attribute type is not set to " + nameof( AttributeTypes.StringAttribute ) );
                    }
                }
                else if( property.Name == "Required" )
                {
                    this.Required = property.ToObject<bool>();
                }
                else if( property.Name == "DefaultValue" )
                {
                    this.DefaultValue = property.ToObject<string>();
                }
                else if( property.Name == "PossibleValues" )
                {
                    // Do nothing for now, this is not set.
                }
            }
        }
    }
}
