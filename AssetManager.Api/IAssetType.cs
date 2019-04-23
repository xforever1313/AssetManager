//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using AssetManager.Api.Attributes.Types;

namespace AssetManager.Api
{
    /// <summary>
    /// Information about an Asset Type.
    /// </summary>
    public interface IAssetType
    {
        // ---------------- Properties ----------------

        /// <summary>
        /// The name of the specific asset type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The database ID this asset type resides in.
        /// </summary>
        Guid DatabaseId { get; set; }

        /// <summary>
        /// Read-only list of the attribute types associated with this asset type.
        /// </summary>
        IReadOnlyList<IAttributeType> AttributeTypes { get; }
    }
}
