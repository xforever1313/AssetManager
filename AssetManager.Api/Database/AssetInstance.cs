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
    /// This is an instance of a specific Asset.
    /// </summary>
    /// <example>
    /// If we are keeping track of video games, this would be the specific
    /// game, whose name would be the Title of the game.
    /// </example>
    public class AssetInstance
    {
        // ---------------- Constructor ----------------

        public AssetInstance()
        {
            this.Name = string.Empty;
        }

        // ---------------- Properties ---------------

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual AssetType AssetType { get; set; }
    }
}
