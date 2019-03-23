//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using Newtonsoft.Json.Linq;

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

        public override string SerializePossibleValues()
        {
            // There no limitations on the possible values.
            return null;
        }

        public override string SerializeDefaultValue()
        {
            return null;
        }

        public override void DeserializeDefaultValue( string data )
        {
            // Ignored, there are no default values.
        }

        public override string Serialize()
        {
            throw new System.NotImplementedException();
        }

        public override void Deserialize( JToken rootNode )
        {
            throw new System.NotImplementedException();
        }
    }
}
