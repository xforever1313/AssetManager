//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Diagnostics;
using AssetManager.Api;
using AssetManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Web.Controllers
{
    public class HomeController : BaseController
    {
        // ----------------- Fields -----------------

        // ----------------- Constructor -----------------

        public HomeController( [FromServices] IAssetManagerApi api ) :
            base( api )
        {
        }

        // ----------------- Functions -----------------

        public IActionResult Index()
        {
            return View( new DefaultModel( this.Api ) );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
