//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.ComponentModel.DataAnnotations;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This class maps which <see cref="Database.AttributeNames"/> go
    /// with which <see cref="AssetType"/>
    /// </summary>
    internal class AssetTypeAttributesMap
    {
        // ---------------- Fields ----------------

        // ---------------- Constructor ----------------

        public AssetTypeAttributesMap()
        {
            this.AttributeType = AttributeTypes.StringAttribute;
        }

        // ---------------- Properties ----------------

        public int Id { get; set; }

        [Required]
        public virtual AssetType AssetType { get; set; }

        [Required]
        public virtual AttributeNames AttributeName { get; set; }

        [Required]
        public virtual AttributeProperties AttributeProperties { get; set; }

        /// <summary>
        /// The type of attribute that is used for the asset type.
        /// Defaulted to <see cref="AttributeTypes.StringAttribute"/>
        /// </summary>
        public AttributeTypes AttributeType { get; set; }
    }
}
