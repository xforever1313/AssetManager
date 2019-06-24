//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api
{
    /// <summary>
    /// How to render an attribute's value.
    /// </summary>
    public enum AttributeTypes
    {
        AssetName = 1,

        /// <summary>
        /// Attribute whose value is a string of any length.
        /// </summary>
        StringAttribute = 2,

        /// <summary>
        /// Attribute whose value is an integer.
        /// </summary>
        Integer = 3,

        /// <summary>
        /// Render the value as an image.
        /// The value is a URL to an image.
        /// </summary>
        ImageUrl = 4,

        /// <summary>
        /// Render the value as an ordered list.
        /// The value is CSV, which needs to be split.
        /// </summary>
        OrderedList = 5,

        /// <summary>
        /// Render the value as an unordered list,
        /// The value is CSV, which needs to be split.
        /// </summary>
        UnorderedList = 6,

        /// <summary>
        /// Render the value as a hyper-link to the specified URL.
        /// </summary>
        Url = 7
    }
}
