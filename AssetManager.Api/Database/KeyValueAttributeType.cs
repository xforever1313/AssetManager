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
    /// These are the type of attributes an asset can have
    /// whose type is of "Key-Value."
    /// 
    /// This class is the name of the "Key".
    /// </summary>
    /// <example>
    /// If we are keeping track of video games, whose <see cref="AssetType"/>
    /// are PC games and Console Games, a Key-Value attribute's Key could be "Console Name"
    /// (for, well, Console Games) or "Operating System" (For PC Games).
    /// The value is defined elsewhere.
    /// </example>
    internal class KeyValueAttributeType
    {
        // ---------------- Constructor ----------------

        public KeyValueAttributeType()
        {
            this.Name = string.Empty;
        }

        // ---------------- Properties ---------------

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
