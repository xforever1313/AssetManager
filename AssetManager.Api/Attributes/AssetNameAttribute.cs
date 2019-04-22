//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Attributes
{
    public class AssetNameAttribute : IAttribute
    {
        // ---------------- Constructor ----------------

        public AssetNameAttribute() :
            this( string.Empty )
        {
        }

        public AssetNameAttribute( string assetName )
        {
            this.Value = assetName;
        }

        // ---------------- Properties ----------------

        AttributeTypes IAttribute.AttributeType
        {
            get
            {
                return AttributeTypes.AssetName;
            }
        }

        /// <summary>
        /// The name of the asset instance.
        /// </summary>
        public string Value { get; set; }

        // ---------------- Functions ----------------

        public override string ToString()
        {
            return this.Value.ToString();
        }

        void IAttribute.Deserialize( string serialData )
        {
            this.Value = serialData ;
        }

        string IAttribute.Serialize()
        {
            return this.Value;
        }
    }
}
