//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Api
{
    /// <summary>
    /// This class helps build attriutes to be used with <see cref="AssetTypeBuilder"/>
    /// </summary>
    public class AttributeBuilder
    {
        // ---------------- Fields ----------------

        private string key;

        // ---------------- Constructor ----------------

        public AttributeBuilder()
        {
            this.Type = AttributeTypes.StringAttribute;
            this.key = "Unknown Key";
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The key that will be used for this attribute.
        /// </summary>
        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                if( string.IsNullOrWhiteSpace( value ) )
                {
                    throw new ArgumentException( nameof( key ) + " can not be null, empty, or whitespace." );
                }
                this.key = value;
            }
        }

        /// <summary>
        /// The type of attribute the attribute will be.
        /// Defaulted to <see cref="AttributeTypes.StringAttribute"/>
        /// </summary>
        public AttributeTypes Type { get; set; }

        // ---------------- Functions ----------------
    }
}
