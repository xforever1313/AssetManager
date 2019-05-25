//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AssetManager.Api.Attributes.Types
{
    public interface IAttributeType<TAttribute> where TAttribute: IAttribute
    {
        /// <summary>
        /// Validates the given attribute against the attribute type.
        /// </summary>
        /// <param name="attr">The attribute to try to validate against the type info.</param>
        /// <returns>An empty enum if there is nothing wrong, else a collection of strings that contain validation errors.</returns>
        IEnumerable<string> TryValidateAttribute( TAttribute attr );

        /// <summary>
        /// Validates the attribute against the attribute type.  If it fails,
        /// a <see cref="SethCS.Exceptions.ListedValidationException"/> is thrown.
        /// </summary>
        void ValidateAttribute( TAttribute attr );
    }

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

        /// <summary>
        /// Validates the given attribute against the attribute type.
        /// </summary>
        /// <param name="attr">The attribute to try to validate against the type info.</param>
        /// <returns>An empty enum if there is nothing wrong, else a collection of strings that contain validation errors.</returns>
        IEnumerable<string> TryValidateAttribute( IAttribute attr );

        /// <summary>
        /// Validates the attribute against the attribute type.  If it fails,
        /// a <see cref="SethCS.Exceptions.ListedValidationException"/> is thrown.
        /// </summary>
        void ValidateAttribute( IAttribute attr );

        // -------- Serialization --------

        /// <summary>
        /// Serializes the possible values the attribute can be
        /// into a string.
        /// </summary>
        string SerializePossibleValues();

        /// <summary>
        /// Takes the default value of the attribute and
        /// converts it to a string.
        /// </summary>
        /// <returns></returns>
        string SerializeDefaultValue();

        /// <summary>
        /// Serializes the entire object into a string.
        /// </summary>
        string Serialize();

        /// <summary>
        /// Overwrite's this object's properties based
        /// on the serialization data passed in.
        /// </summary>
        void Deserialize( string data );

        /// <summary>
        /// Converts the given string of the possible values
        /// and sets them inside of this class.
        /// </summary>
        void DeserializePossibleValues( string data );

        /// <summary>
        /// Converts the given string of the default value and sets
        /// it to the attribute's default value.
        /// </summary>
        void DeserializeDefaultValue( string data );

        /// <summary>
        /// Overwrite's this object's properties based
        /// on the serialization data passed in.
        /// </summary>
        void Deserialize( JToken data );
    }
}
