//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Text;

namespace AssetManager.Api.Attributes.Types
{
    public abstract class BaseAttributeType : IAttributeType
    {
        // ---------------- Constructor ----------------

        protected BaseAttributeType()
        {
        }

        // ---------------- Properties ----------------

        public string Key { get; set; }

        // ---------------- Functions ----------------

        public void Validate()
        {
            bool success = true;
            StringBuilder builder = new StringBuilder();

            builder.AppendLine( "Can not validate attribute:" );
            if( string.IsNullOrWhiteSpace( this.Key ) )
            {
                success = false;
                builder.AppendLine( "- Key can not be null, empty, whitespace." );
            }
            if( this.ValidateInternal( out string errors ) == false )
            {
                success = false;
                builder.AppendLine( errors );
            }

            if( success == false )
            {
                throw new InvalidOperationException( builder.ToString() );
            }
        }

        protected abstract bool ValidateInternal( out string errors );
    }
}
