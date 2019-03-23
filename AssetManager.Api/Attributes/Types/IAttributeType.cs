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

        /// <summary>
        /// Is this attribute required to be specified by the user?
        /// </summary>
        bool Required { get; set; }

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

        // -------- Serialization --------

        /// <summary>
        /// Serializes the possible values the attribute can be
        /// into a string.
        /// </summary>
        string SerializePossibleValues();

        /// <summary>
        /// Serializes the entire object into a string.
        /// </summary>
        string Serialize();

        /// <summary>
        /// Overwrite's this object's properties based
        /// on the serialization data passed in.
        /// </summary>
        void Deserialize( string data );
    }
}
