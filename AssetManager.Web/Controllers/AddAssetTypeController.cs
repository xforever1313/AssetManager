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
    public class AddAssetTypeController : BaseController
    {
        // ---------------- Constructor ----------------

        public AddAssetTypeController( [FromServices] IAssetManagerApi api ) :
            base( api )
        {
        }

        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            return View( this.Api );
        }

        [HttpPost]
        public IActionResult AddAssetType( [FromBody] AssetTypeBuilderModel maker )
        {
            this.Api.DataBase.AddAssetType( maker );

            return this.Redirect( "/" );
        }
    }
}