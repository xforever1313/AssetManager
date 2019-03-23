//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Attributes.Types
{
    public class AssetNameAttributeType : BaseAttributeType
    {
        // ---------------- Constructor ----------------

        public AssetNameAttributeType() :
            base( AttributeTypes.AssetName )
        {
        }

        // ---------------- Functions ----------------

        protected override bool ValidateInternal( out string errors )
        {
            // Nothing to validate.
            errors = string.Empty;
            return true;
        }

        public override void Deserialize( string data )
        {
            throw new System.NotImplementedException();
        }

        public override string SerializePossibleValues()
        {
            throw new System.NotImplementedException();
        }

        public override string Serialize()
        {
            throw new System.NotImplementedException();
        }

        public override string SerializeDefaultValue()
        {
            throw new System.NotImplementedException();
        }

        public override void DeserializeDefaultValue( string data )
        {
            throw new System.NotImplementedException();
        }
    }
}
