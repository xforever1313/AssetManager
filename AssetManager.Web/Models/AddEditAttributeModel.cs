//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;
using AssetManager.Api.Attributes.Types;

namespace AssetManager.Web.Models
{
    public class AddEditAttributeModel<TAttributeType, TAttribute> 
        where TAttributeType : IAttributeType
        where TAttribute : IAttribute
    {
        // ---------------- Constructor ----------------

        public AddEditAttributeModel( TAttributeType attributeType, TAttribute attribute, bool editing )
        {
            this.AttributeType = attributeType;
            this.Attribute = attribute;
            this.IsEditing = editing;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The attribute type definition for the attribute.
        /// </summary>
        public TAttributeType AttributeType { get; private set; }

        /// <summary>
        /// The attribute information.
        /// </summary>
        public TAttribute Attribute { get; private set; }

        /// <summary>
        /// Are we editing an attribute or adding it?
        /// </summary>
        public bool IsEditing { get; private set; }
    }
}
