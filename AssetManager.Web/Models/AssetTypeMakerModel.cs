//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManager.Api;
using AssetManager.Api.Attributes.Types;

namespace AssetManager.Web.Models
{
    public class AssetTypeMakerModel
    {
        // ---------------- Fields ----------------

        // ---------------- Constructor ----------------

        public AssetTypeMakerModel()
        {
        }

        // ---------------- Properties ----------------

        public AttributeTypeModel[] AttributeList { get; set; }

        // ---------------- Functions ----------------
    }
}
