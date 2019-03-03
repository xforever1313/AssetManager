//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This is the actual values of the key-value attributes
    /// for an INSTANCE of an asset.
    /// </summary>
    /// <example>
    /// When sorting video games, a PC game's Key of "Operating System"
    /// would have the value "Windows 95" in this table.
    /// </example>
    internal class AssetInstanceKeyValueAttributeValues
    {
        // ---------------- Constructor ----------------

        public AssetInstanceKeyValueAttributeValues()
        {
        }

        // ---------------- Properties ----------------

        public int Id { get; set; }

        /// <summary>
        /// The instance of an asset this key-value attribute
        /// is associated with.
        /// </summary>
        public virtual AssetInstance AssetInstance { get; set; }

        /// <summary>
        /// The associated Key that this value is tied to.
        /// </summary>
        public virtual KeyValueAttributeType Key { get; set; }

        /// <summary>
        /// The associated value.
        /// </summary>
        public string Value { get; set; }
    }
}
