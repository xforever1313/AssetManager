//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AssetManager.Api.Attributes.Types
{
    /// <summary>
    /// This attribute type is a URL to an image.
    /// </summary>
    public class ImageUrlAttributeType : BaseAttributeType<ImageUrlAttribute>
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// Must either start with https:, http:, or /.
        /// </summary>
        private static readonly Regex allowedRegex;

        // ---------------- Constructor ----------------

        public ImageUrlAttributeType() :
            base( AttributeTypes.ImageUrl )
        {
        }

        static ImageUrlAttributeType()
        {
            allowedRegex = new Regex(
                "^(http:|https:|/)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
            );
        }

        // ---------------- Functions ----------------

        public override IEnumerable<string> TryValidateAttribute( ImageUrlAttribute attr )
        {
            List<string> errors = new List<string>();

            if ( this.Required && string.IsNullOrWhiteSpace( attr.Value ) )
            {
                errors.Add( "Attribute is marked as required, but value is null, empty, or whitespace" );
            }
            else if ( string.IsNullOrWhiteSpace( attr.Value ) == false )
            {
                Match match = allowedRegex.Match( attr.Value );
                if ( match.Success == false )
                {
                    errors.Add( "URL must start with 'https:', 'http:', or '/'." );
                }
            }

            if ( attr.Height.HasValue && ( attr.Height < 0 ) )
            {
                errors.Add( "Height can not be negative, got: " + attr.Height.Value );
            }

            if ( attr.Width.HasValue && ( attr.Width < 0 ) )
            {
                errors.Add( "Width can not be negative, got: " + attr.Width.Value );
            }

            return errors;
        }

        protected override IEnumerable<string> ValidateInternal()
        {
            // Nothing to validate; there are no settings to add.
            return new List<string>();
        }

        public override string SerializePossibleValues()
        {
            return null;
        }


        public override string SerializeDefaultValue()
        {
            // No default value.
            return null;
        }

        public override void DeserializePossibleValues( string data )
        {
            // Nothing to do.
        }

        public override void DeserializeDefaultValue( string data )
        {
            // Nothing to do.
        }

        public override string Serialize()
        {
            JObject root = new JObject
            {
                ["Key"] = this.Key,
                ["AttributeType"] = Convert.ToInt32( this.AttributeType ),
                ["Required"] = this.Required,
                ["DefaultValue"] = this.SerializeDefaultValue(),
                ["PossibleValues"] = this.SerializePossibleValues()
            };

            return root.ToString();
        }

        public override void Deserialize( JToken rootNode )
        {
            foreach ( JProperty property in rootNode.Children<JProperty>() )
            {
                if ( property.Name == "Key" )
                {
                    this.Key = property.ToObject<string>();
                }
                else if ( property.Name == "AttributeType" )
                {
                    // Sanity check, make sure we are an int.
                    AttributeTypes type = (AttributeTypes)property.ToObject<long>();
                    if ( type != base.AttributeType )
                    {
                        throw new ValidationException( "Attribute type is not set to " + base.AttributeType );
                    }
                }
                else if ( property.Name == "Required" )
                {
                    this.Required = property.ToObject<bool>();
                }
                else if ( property.Name == "DefaultValue" )
                {
                    // Do nothing for now, this is not set.
                }
                else if ( property.Name == "PossibleValues" )
                {
                    // Do nothing for now, this is not set.
                }
            }
        }
    }
}
