//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace AssetManager.Api.Attributes.Types
{
    public static class AttributeTypeFactory
    {
        /// <summary>
        /// Creates a default attribute type instance of the passed in type.
        /// </summary>
        public static IAttributeType CreateAttributeType( AttributeTypes type )
        {
            switch( type )
            {
                case AttributeTypes.AssetName:
                    return new AssetNameAttributeType();

                case AttributeTypes.Integer:
                    return new IntegerAttributeType();

                case AttributeTypes.StringAttribute:
                    return new StringAttributeType();

                default:
                    throw new NotImplementedException( type.ToString() + " has not been implemented yet." );
            }
        }
    }
}
