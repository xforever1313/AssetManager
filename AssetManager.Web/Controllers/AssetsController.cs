//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;
using AssetManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Web.Controllers
{
    public class AssetsController : BaseController
    {
        public AssetsController( [FromServices] IAssetManagerApi api ) :
            base( api )
        {
        }

        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            return View( new DefaultModel( this.Api ) );
        }

        public IActionResult List( string id )
        {
            AssetListModel model = new AssetListModel( this.Api, id );
            return View( model );
        }
    }
}