//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Net;
using AssetManager.Api;
using AssetManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SethCS.Exceptions;

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

        // ---------------- Functions ----------------

        protected IActionResult SafePerformAction( Func<IActionResult> action )
        {
            try
            {
                IActionResult result = action();
                return result;
            }
            catch ( ValidationException e )
            {
                ErrorModel model = new ErrorModel( this.Api )
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = e.Message
                };
                return View( "AssetManagerError", model );
            }
            catch ( ArgumentException e )
            {
                ErrorModel model = new ErrorModel( this.Api )
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = e.Message
                };
                return View( "AssetManagerError", model );
            }
            catch ( Exception e )
            {
                ErrorModel model = new ErrorModel( this.Api )
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Message = e.Message
                };
                return View( "AssetManagerError", model );
            }
        }
    }
}
