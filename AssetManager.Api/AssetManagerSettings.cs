//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Api
{
    public class AssetManagerSettings
    {
        // ---------------- Constructor ----------------

        public AssetManagerSettings()
        {
        }

        // ---------------- Properties ----------------

        public string AssetManagerSettingsDirectory { get; set; }

        /// <summary>
        /// The path to the assembly that contains the database configuration.
        /// </summary>
        public string DatabaseAssemblyPath { get; set; }

        // ---------------- Functions ----------------

        public void Validate()
        {
        }
    }
}
