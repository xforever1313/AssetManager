//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SethCS.Exceptions;

namespace AssetManager.Api.Attributes.Types
{
    public abstract class BaseAttributeType<TAttribute> : IAttributeType, IAttributeType<TAttribute> where TAttribute : IAttribute
    {
        // ---------------- Constructor ----------------

        protected BaseAttributeType( AttributeTypes attributeType )
        {
            this.AttributeType = attributeType;
        }

        // ---------------- Properties ----------------

        public string Key { get; set; }

        public AttributeTypes AttributeType { get; private set; }

        /// <summary>
        /// Is this attribute required to be specified by the user?
        /// </summary>
        public bool Required { get; set; }

        // ---------------- Functions ----------------

        public IEnumerable<string> TryValidate()
        {
            List<string> errors = new List<string>();

            if( string.IsNullOrWhiteSpace( this.Key ) )
            {
                errors.Add( "Key can not be null, empty, whitespace." );
            }

            errors.AddRange( this.ValidateInternal() );

            return errors;
        }

        public void Validate()
        {
            IEnumerable<string> errors = this.TryValidate();
            if ( errors.Count() > 0 )
            {
                throw new ListedValidationException(
                    "Can not validate " + this.AttributeType.ToString() + ":",
                    errors
                );
            }
        }

        /// <summary>
        /// Ensures the given attribute follows all of the rules defined
        /// in this attribute type.
        /// </summary>
        /// <exception cref="ArgumentException">If the attribute type is not the same as this one's</exception>
        /// <returns>List of errors that are wrong, the list ie empty if there are none.</returns>
        public IEnumerable<string> TryValidateAttribute( IAttribute attr )
        {
            if ( this.AttributeType != attr.AttributeType )
            {
                throw new ArgumentException(
                    "Passed in attribute is of type " + attr.AttributeType + ", must be of type " + this.AttributeType,
                    nameof( attr )
                );
            }

            return TryValidateAttribute( (TAttribute)attr );
        }

        /// <summary>
        /// Ensures the given attribute follows all of the rules defined
        /// in this attribute type.
        /// </summary>
        /// <exception cref="ListedValidationException">If the given attribute breaks any rules.</exception>
        public void ValidateAttribute( IAttribute attr )
        {
            IEnumerable<string> errors = TryValidateAttribute( attr );
            if ( errors.Count() > 0 )
            {
                throw new ListedValidationException( "Attribute is incompatible with Attribute Type " + this.Key, errors );
            }
        }

        /// <summary>
        /// Ensures the given attribute follows all of the rules defined
        /// in this attribute type.
        /// </summary>
        /// <exception cref="ArgumentException">If the attribute type is not the same as this one's</exception>
        /// <returns>List of errors that are wrong, the list ie empty if there are none.</returns>

        public abstract IEnumerable<string> TryValidateAttribute( TAttribute attr );

        /// <summary>
        /// Ensures the given attribute follows all of the rules defined
        /// in this attribute type.
        /// </summary>
        /// <exception cref="ListedValidationException">If the given attribute breaks any rules.</exception>
        public void ValidateAttribute( TAttribute attr )
        {
            IEnumerable<string> errors = TryValidateAttribute( attr );
            if ( errors.Count() > 0 )
            {
                throw new ListedValidationException( "Attribute is incompatible with Attribute Type " + this.Key, errors );
            }
        }

        public abstract string SerializePossibleValues();

        public abstract string SerializeDefaultValue();

        public abstract void DeserializePossibleValues( string data );

        public abstract void DeserializeDefaultValue( string data );

        public abstract string Serialize();

        public void Deserialize( string data )
        {
            this.Deserialize( JToken.Parse( data ) );
        }

        protected abstract IEnumerable<string> ValidateInternal();

        public abstract void Deserialize( JToken data );
    }
}
