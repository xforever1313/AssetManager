﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using SethCS.Exceptions;

namespace AssetManager.Api.Attributes.Types
{
    public class IntegerAttributeType : BaseAttributeType<IntegerAttribute>
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// Serialization schema version.
        /// </summary>
        private const int schemaVersion = 1;

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

        protected override IEnumerable<string> ValidateInternal()
        {
            List<string> errors = new List<string>();

            if( this.MinValue.HasValue && this.MaxValue.HasValue )
            {
                if( this.MinValue.Value > this.MaxValue )
                {
                    errors.Add( "Min value can not be greater than the maximum value." );
                }
            }

            if( this.DefaultValue.HasValue )
            {
                if( this.MinValue.HasValue && ( this.MinValue.Value > this.DefaultValue.Value ) )
                {
                    errors.Add( "The default value can not be less than the minimum value." );
                }

                if( this.MaxValue.HasValue && ( this.MaxValue.Value < this.DefaultValue.Value ) )
                {
                    errors.Add( "The default value can not be greater than the maximum value." );
                }
            }

            return errors;
        }

        public override IEnumerable<string> TryValidateAttribute( IntegerAttribute attr )
        {
            List<string> errors = new List<string>();

            if ( this.MinValue.HasValue && ( this.MinValue.Value > attr.Value ) )
            {
                errors.Add(
                    string.Format( "Value is less than the minimum of {0}, got {1}", this.MinValue, attr.Value )
                );
            }

            if ( this.MaxValue.HasValue && ( this.MaxValue.Value < attr.Value ) )
            {
                errors.Add(
                    string.Format( "Value is greater than the value of {0}, got {1}", this.MaxValue, attr.Value )
                );
            }

            return errors;
        }

        public override string SerializePossibleValues()
        {
            return this.SerializePossibleValuesToJson().ToString();
        }

        private JObject SerializePossibleValuesToJson()
        {
            JObject possibleValues = new JObject
            {
                ["Version"] = schemaVersion,
                ["MinValue"] = this.MinValue,
                ["MaxValue"] = this.MaxValue
            };

            return possibleValues;
        }

        public override void DeserializePossibleValues( string data )
        {
            JObject token = JObject.Parse( data );
            DeserializePossibleValuesJson( token );
        }

        private void DeserializePossibleValuesJson( JObject possibleValuesObject )
        {
            foreach ( JProperty possibleValueProperty in possibleValuesObject.Children<JProperty>() )
            {
                if ( possibleValueProperty.Name == "MinValue" )
                {
                    this.MinValue = possibleValueProperty.ToObject<int?>();
                }

                if ( possibleValueProperty.Name == "MaxValue" )
                {
                    this.MaxValue = possibleValueProperty.ToObject<int?>();
                }
            }
        }

        public override void DeserializeDefaultValue( string data )
        {
            if( data == null )
            {
                this.DefaultValue = null;
            }
            else
            {
                this.DefaultValue = int.Parse( data );
            }
        }

        public override string SerializeDefaultValue()
        {
            return this.DefaultValue.HasValue ? this.DefaultValue.Value.ToString() : null;
        }

        public override string Serialize()
        {
            JObject root = new JObject
            {
                ["Key"] = this.Key,
                ["AttributeType"] = Convert.ToInt32( this.AttributeType ),
                ["Required"] = this.Required,
                ["DefaultValue"] = this.DefaultValue,
                ["PossibleValues"] = SerializePossibleValuesToJson()
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
                    if( type != AttributeTypes.Integer )
                    {
                        throw new ValidationException( "Attribute type is not set to " + nameof( AttributeTypes.Integer ) );
                    }
                }
                else if( property.Name == "Required" )
                {
                    this.Required = property.ToObject<bool>();
                }
                else if( property.Name == "DefaultValue" )
                {
                    this.DefaultValue = property.ToObject<int?>();
                }
                else if( property.Name == "PossibleValues" )
                {
                    if( property.First is JObject possibleValuesObject )
                    {
                        DeserializePossibleValuesJson( possibleValuesObject );
                    }
                }
            }
        }
    }
}
