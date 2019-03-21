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

        /// <summary>
        /// The type of attribute it is.
        /// </summary>
        AttributeTypes AttributeType { get; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Trys to validate the attribute.
        /// If the attribute is not valid, this returns false,
        /// and the errors get outtputted.
        /// </summary>
        /// <param name="errors">The errors.  Empty string if there are none.</param>
        /// <returns>True if the object is valid, else false.</returns>
        bool TryValidate( out string errors );

        /// <summary>
        /// Ensures the attribute is in an okay state.
        /// If not, a <see cref="SethCS.Exceptions.ValidationException"/> is thrown.
        /// </summary>
        void Validate();
    }
}
