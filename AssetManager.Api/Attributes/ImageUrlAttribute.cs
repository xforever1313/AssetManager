//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using Newtonsoft.Json.Linq;

namespace AssetManager.Api.Attributes
{
    /// <summary>
    /// This attribute is a URL to an image.
    /// </summary>
    public class ImageUrlAttribute : IAttribute, IEquatable<ImageUrlAttribute>
    {
        // ---------------- Fields ----------------

        private const int schemaVersion = 1;

        // ---------------- Constructor ----------------

        /// <summary>
        /// Default constructor, initial value is an empty string.
        /// </summary>
        public ImageUrlAttribute() :
            this( string.Empty )
        {
        }

        public ImageUrlAttribute( string value )
        {
            this.Value = value;
        }

        // ---------------- Properties ----------------

        public string Value { get; set; }

        /// <summary>
        /// Image Width.  If set to null, this will use the image's width in the front-end,
        /// not one that is user-defined.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Image Height.  If set to null, this will use the image's height in the front-end,
        /// not one that is user-defined.
        /// </summary>
        public int? Height { get; set; }

        AttributeTypes IAttribute.AttributeType
        {
            get
            {
                return AttributeTypes.ImageUrl;
            }
        }

        // ---------------- Functions ----------------

        public override bool Equals( object obj )
        {
            return this.Equals( obj as ImageUrlAttribute );
        }

        public bool Equals( ImageUrlAttribute other )
        {
            if ( other == null )
            {
                return false;
            }

            return
                ( this.Width == other.Width ) &&
                ( this.Height == other.Height ) &&
                ( this.Value == other.Value );
        }

        public override int GetHashCode()
        {
            return
                ( this.Width.HasValue ? this.Width.GetHashCode() : 0 ) +
                ( this.Height.HasValue ? this.Height.GetHashCode() : 0 ) +
                ( ( this.Value != null ) ? this.Value.GetHashCode() : 0 );
        }

        public override string ToString()
        {
            IAttribute attr = this;
            return attr.Serialize();
        }

        void IAttribute.Deserialize( string serialData )
        {
            JToken rootNode = JToken.Parse( serialData );
            foreach ( JProperty property in rootNode.Children<JProperty>() )
            {
                if ( property.Name == "Width" )
                {
                    this.Width = property.ToObject<int?>();
                }
                else if ( property.Name == "Height" )
                {
                    this.Height = property.ToObject<int?>();
                }
                else if ( property.Name == "Value" )
                {
                    this.Value = property.ToObject<string>();
                }
            }
        }

        string IAttribute.Serialize()
        {
            JObject root = new JObject
            {
                ["SchemaVersion"] = schemaVersion,
                ["Width"] = this.Width,
                ["Height"] = this.Height,
                ["Value"] = this.Value
            };

            return root.ToString();
        }
    }
}
