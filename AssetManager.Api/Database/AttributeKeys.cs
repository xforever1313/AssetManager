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
    /// These are the keys for the attribute values.  This incldues the attribute type.
    /// </summary>
    /// <example>
    /// If we are keeping track of video games, whose <see cref="AssetType"/>
    /// are PC games and Console Games, an attribute name could be "Console Name"
    /// (for, well, Console Games) or "Operating System" (For PC Games).
    /// The value is defined elsewhere.
    /// </example>
    internal class AttributeKeys
    {
        // ---------------- Constructor ----------------

        public AttributeKeys()
        {
            this.Name = string.Empty;
            this.AttributeType = AttributeTypes.StringAttribute;
        }

        // ---------------- Properties ---------------

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The type of attribute that is used for the asset type.
        /// Defaulted to <see cref="AttributeTypes.StringAttribute"/>
        /// </summary>
        public AttributeTypes AttributeType { get; set; }
    }
}
