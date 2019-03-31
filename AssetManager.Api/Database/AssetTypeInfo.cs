//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Api.Database
{
    /// <summary>
    /// This class contains basic information about an <see cref="AssetType"/>
    /// </summary>
    public class AssetTypeInfo
    {
        // ---------------- Constructor ----------------

        public AssetTypeInfo( string name, int numberOfAssets )
        {
            this.Name = name;
            this.NumberOfAssets = numberOfAssets;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The name of the asset type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The number of assets inside of this asset type.
        /// </summary>
        public int NumberOfAssets { get; private set; }
    }
}
