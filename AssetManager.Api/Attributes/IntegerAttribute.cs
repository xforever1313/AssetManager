//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Attributes
{
    /// <summary>
    /// An attribute whose value is an integer.
    /// </summary>
    public class IntegerAttribute : IAttribute
    {
        // ---------------- Constructor ----------------

        /// <summary>
        /// Constructor that sets the inital value to 0.
        /// </summary>
        public IntegerAttribute() :
            this( 0 )
        {
        }

        /// <summary>
        /// Constructor that sets the inital value to the passed
        /// in parameter.
        /// </summary>
        public IntegerAttribute( int value )
        {
            this.Value = value;
        }

        // ---------------- Properties ----------------

        AttributeTypes IAttribute.AttributeType
        {
            get
            {
                return AttributeTypes.Integer;
            }
        }

        public int Value { get; set; }

        // ---------------- Functions ----------------

        void IAttribute.Deserialize( string serialData )
        {
            this.Value = int.Parse( serialData );
        }

        string IAttribute.Serialize()
        {
            return this.Value.ToString();
        }
    }
}
