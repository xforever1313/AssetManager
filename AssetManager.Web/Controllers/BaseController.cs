//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Web.Controllers
{
    public class BaseController : Controller
    {
        // ---------------- Constructor ----------------

        protected BaseController( IAssetManagerApi api )
        {
            this.Api = api;
        }

        // ---------------- Properties ----------------

        public IAssetManagerApi Api { get; private set; }
    }
}
