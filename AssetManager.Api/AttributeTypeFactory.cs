//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using AssetManager.Api.Attributes.Types;

namespace AssetManager.Api
{
    internal static class AttributeTypeFactory
    {
        public static TAttrType CreateAttributeType<TAttrType>() where TAttrType : IAttributeType
        {
            return Activator.CreateInstance<TAttrType>();
        }

        internal static IAttributeType CreateAttributeType( AttributeTypes attributeType )
        {
            switch ( attributeType )
            {
                case AttributeTypes.StringAttribute:
                    return new StringAttributeType();

                case AttributeTypes.Integer:
                    return new IntegerAttributeType();

                case AttributeTypes.AssetName:
                    return new AssetNameAttributeType();

                default:
                    throw new ArgumentException( "Invalid Attribute Type: " + attributeType );
            }
        }
    }
}
