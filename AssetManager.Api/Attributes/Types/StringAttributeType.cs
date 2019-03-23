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
    }

    /// <summary>
    /// This converts <see cref="StringAttributeType"/> to/from JSON
    /// </summary>
    /// <example>
    /// {
    ///     "Key": "My Attribute",
    ///     "AttributeType": 2,
    ///     "Required": false,
    ///     "PossibleValues": null,
    ///     "DefaultValue": "Hello"
    /// };
    /// </example>
    public static class StringAttributeTypeSerialzier
    {
        public static JObject SerializePossibleValues( this StringAttributeType attr )
        {
            // No possible values property on string attribute types, return null.
            return null;
        }

        public static JObject Serialize( this StringAttributeType attr )
        {
            JObject root = new JObject
            {
                ["Key"] = attr.Key,
                ["AttributeType"] = Convert.ToInt32( attr.AttributeType ),
                ["Required"] = attr.Required,
                ["DefaultValue"] = attr.DefaultValue,
                ["PossibleValues"] = SerializePossibleValues( attr )
            };

            return root;
        }

        public static void Deserialize( this StringAttributeType attr, JToken rootNode )
        {
            // Do no validation here, that is up to the class itself.  If something is null or missing, so be it.

            foreach( JProperty property in rootNode.Children<JProperty>() )
            {
                if( property.Name == "Key" )
                {
                    attr.Key = property.ToObject<string>();
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
                    attr.Required = property.ToObject<bool>();
                }
                else if( property.Name == "DefaultValue" )
                {
                    attr.DefaultValue = property.ToObject<string>();
                }
                else if( property.Name == "PossibleValues" )
                {
                    // Do nothing for now, this is not set.
                }
            }
        }
    }
}
