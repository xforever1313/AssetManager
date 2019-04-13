//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Attributes
{
    /// <summary>
    /// This is the "Default Attribute", which is an attribute
    /// that is simply a string value that gets tied to a key.
    /// </summary>
    public class StringAttribute : IAttribute
    {
        // ---------------- Constructor ----------------

        /// <summary>
        /// Constructor that sets the initial value to empty string.
        /// </summary>
        public StringAttribute() :
            this( string.Empty )
        {
        }

        /// <summary>
        /// Constructor that sets the inital value to the passed
        /// in parameter.
        /// </summary>
        public StringAttribute( string value )
        {
            this.Value = value;
        }

        // ---------------- Properties ----------------

        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }

        AttributeTypes IAttribute.AttributeType
        {
            get
            {
                return AttributeTypes.StringAttribute;
            }
        }

        // ---------------- Functions ----------------

        void IAttribute.Deserialize( string serialData )
        {
            this.Value = serialData;
        }

        string IAttribute.Serialize()
        {
            return this.Value;
        }
    }
}
