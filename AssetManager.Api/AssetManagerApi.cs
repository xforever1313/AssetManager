//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace AssetManager.Api
{
    public class AssetManagerApi
    {
        // ---------------- Constructor ----------------

        public AssetManagerApi()
        {
            this.DataBase = new Database.DatabaseApi();
        }

        // ---------------- Properties ----------------

        public Database.DatabaseApi DataBase { get; private set; }
    }
}
