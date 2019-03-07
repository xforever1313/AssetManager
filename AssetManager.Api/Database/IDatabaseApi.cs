﻿//
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
    public interface IDatabaseApi
    {
        // ---------------- Functions ----------------

        void AddAssetType( AssetTypeBuilder builder );

        Asset GenerateEmptyAsset( string assetTypeName );

        void AddAsset( Asset asset );
    }
}