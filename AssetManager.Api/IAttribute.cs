//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

namespace AssetManager.Api
{
    public interface IAttribute
    {
        // ---------------- Properties ----------------

        AttributeTypes AttributeType { get; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Takes the attribute and serializes it to a string that can later
        /// be used to recreate it.
        /// </summary>
        string Serialize();

        /// <summary>
        /// Deserializes the passed in data and overwrites this instance's
        /// properties based on the serial data.
        /// </summary>
        void Deserialize( string serialData );
    }
}
