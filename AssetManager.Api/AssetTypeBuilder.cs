//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using System.Text;
using AssetManager.Api.Attributes.Types;
using SethCS.Exceptions;

namespace AssetManager.Api
{
    /// <summary>
    /// This class aides in building an Asset Type.
    /// </summary>
    public class AssetTypeBuilder
    {
        // ---------------- Fields ----------------

        internal const string UnknownType = "Unknown Asset";

        // ---------------- Constructor ----------------

        public AssetTypeBuilder() :
            this( UnknownType )
        {
        }

        public AssetTypeBuilder( string assetName )
        {
            this.Name = assetName;
            this.AttributeTypes = new List<IAttributeType>();
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The name of the specific asset instance.
        /// </summary>
        public string Name { get; private set; }

        public IList<IAttributeType> AttributeTypes { get; protected set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Validates both this class and all the attributes.
        /// Throws a <see cref="ValidationException"/>
        /// if this object is not valid.
        /// </summary>
        public void Validate()
        {
            bool success = true;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine( "Errors when validating the Asset Type Builder:" );

            if( string.IsNullOrWhiteSpace( this.Name ) )
            {
                success = false;
                builder.AppendLine( "- Name can not be null, empty, or whitespace." );
            }

            foreach( IAttributeType attribute in AttributeTypes )
            {
                if( attribute.TryValidate( out string attrErrors ) == false )
                {
                    success = false;
                    builder.AppendLine( attrErrors );
                }
            }

            if( success == false )
            {
                throw new ValidationException( builder.ToString() );
            }
        }
    }
}
