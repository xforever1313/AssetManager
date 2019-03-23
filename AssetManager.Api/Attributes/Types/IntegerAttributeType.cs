//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Text;
using Newtonsoft.Json.Linq;
using SethCS.Exceptions;

namespace AssetManager.Api.Attributes.Types
{
    public class IntegerAttributeType : BaseAttributeType
    {
        // ---------------- Constructor ----------------

        public IntegerAttributeType() :
            base( AttributeTypes.Integer )
        {
        }

        /// <summary>
        /// The minimum value this can be set to.
        /// Defaulted to not-specified.
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// The maximum value this can be set to.
        /// Defaulted to not-specified.
        /// </summary>
        public int? MaxValue { get; set; }

        /// <summary>
        /// The default value to set the integer to.
        /// Defaulted to none.
        /// </summary>
        public int? DefaultValue { get; set; }

        // ---------------- Functions ----------------

        protected override bool ValidateInternal( out string errors )
        {
            bool success = true;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine( "Errors when validating " + nameof( IntegerAttributeType ) );

            if( this.MinValue.HasValue && this.MaxValue.HasValue )
            {
                if( this.MinValue.Value > this.MaxValue )
                {
                    success = false;
                    builder.AppendLine( "- Min value can not be greater than the maximum value." );
                }
            }

            if( this.DefaultValue.HasValue )
            {
                if( this.MinValue.HasValue && ( this.MinValue.Value > this.DefaultValue.Value ) )
                {
                    success = false;
                    builder.AppendLine( "- Min value can not be greater than the default value." );
                }

                if( this.MaxValue.HasValue && ( this.MaxValue.Value < this.DefaultValue.Value ) )
                {
                    success = false;
                    builder.AppendLine( "- Max value can not be less than the default value." );
                }
            }

            if( success )
            {
                errors = string.Empty;
            }
            else
            {
                errors = builder.ToString();
            }

            return success;
        }
    }

    /// <summary>
    /// This converts <see cref="IntegerAttributeType"/> to/from JSON.
    /// </summary>
    /// <example>
    /// {
    ///     Key: Name,
    ///     AttributeType: 3,
    ///     Properties: {
    ///         Required: False,
    ///         PossibleValues: {
    ///             Version: 1,
    ///             MinValue: 3,
    ///             MaxValue: null
    ///         },
    ///         DefaultValue: null
    ///     }
    /// }
    /// </example>
    public static class IntergerAttributeTypeSerializer
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// In case our JSON schema ever changes...
        /// </summary>
        private const int schemaVersion = 1;

        // ---------------- Properties ----------------

        public static JObject SerializePossibleValues( this IntegerAttributeType attr )
        {
            JObject possibleValues = new JObject
            {
                ["Version"] = schemaVersion,
                ["MinValue"] = attr.MinValue,
                ["MaxValue"] = attr.MaxValue
            };

            return possibleValues;
        }

        public static JObject Serialize( this IntegerAttributeType attr )
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

        public static void Deserialize( this IntegerAttributeType attr, JToken rootNode )
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
                    if( type != AttributeTypes.Integer )
                    {
                        throw new ValidationException( "Attribute type is not set to " + nameof( AttributeTypes.Integer ) );
                    }
                }
                else if( property.Name == "Required" )
                {
                    attr.Required = property.ToObject<bool>();
                }
                else if( property.Name == "DefaultValue" )
                {
                    attr.DefaultValue = property.ToObject<int?>();
                }
                else if( property.Name == "PossibleValues" )
                {
                    if( property.First is JObject possibleValuesObject )
                    {
                        foreach( JProperty possibleValueProperty in possibleValuesObject.Children<JProperty>() )
                        {
                            if( possibleValueProperty.Name == "MinValue" )
                            {
                                attr.MinValue = possibleValueProperty.ToObject<int?>();
                            }

                            if( possibleValueProperty.Name == "MaxValue" )
                            {
                                attr.MaxValue = possibleValueProperty.ToObject<int?>();
                            }
                        }
                    }
                }
            }
        }
    }
}
