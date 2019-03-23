//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Text;
using SethCS.Exceptions;

namespace AssetManager.Api.Attributes.Types
{
    public abstract class BaseAttributeType : IAttributeType
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

        public bool TryValidate( out string errors )
        {
            bool success = true;
            StringBuilder builder = new StringBuilder();

            builder.AppendLine( "Can not validate " + this.AttributeType.ToString() + ":" );
            if( string.IsNullOrWhiteSpace( this.Key ) )
            {
                success = false;
                builder.AppendLine( "- Key can not be null, empty, whitespace." );
            }
            if( this.ValidateInternal( out string internalErrors ) == false )
            {
                success = false;
                builder.AppendLine( internalErrors );
            }

            if( success )
            {
                errors = string.Empty;
            }
            else
            {
                errors = builder.ToString();
            }

            return success;
        }

        public void Validate()
        {
            if( this.TryValidate( out string errors ) == false )
            {
                throw new ValidationException( errors );
            }
        }

        public abstract string SerializePossibleValues();

        public abstract string SerializeDefaultValue();

        public abstract void DeserializeDefaultValue( string data );

        public abstract string Serialize();

        public abstract void Deserialize( string data );

        protected abstract bool ValidateInternal( out string errors );
    }
}
