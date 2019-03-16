//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Attributes.Types
{
    public interface IAttributeType
    {
        // ---------------- Properties ----------------

        /// <summary>
        /// The "Key" of the attribute.
        /// </summary>
        string Key { get; set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Ensures the attribute is in an okay state.
        /// </summary>
        void Validate();
    }
}
