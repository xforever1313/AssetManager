﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using AssetManager.Api.Attributes;

namespace AssetManager.Api
{
    internal static class AttributeFactory
    {
        public static TAttr CreateAttribute<TAttr>() where TAttr : IAttribute
        {
            return Activator.CreateInstance<TAttr>();
        }

        internal static IAttribute CreateAttribute( AttributeTypes attributeType )
        {
            switch( attributeType )
            {
                case AttributeTypes.StringAttribute:
                    return new StringAttribute();

                case AttributeTypes.Integer:
                    return new IntegerAttribute();

                default:
                    throw new ArgumentException( "Invalid Attribute Type: " + attributeType );
            }
        }
    }
}