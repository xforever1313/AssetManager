//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api.Database;

namespace AssetManager.Api
{
    public interface IAssetManagerApi
    {
        // ---------------- Properties ----------------

        IDatabaseApi DataBase { get; }

        IAsyncDatabaseApi AsyncDataBase { get; }
    }
}
