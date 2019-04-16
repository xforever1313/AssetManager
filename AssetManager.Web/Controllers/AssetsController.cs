//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using AssetManager.Api;
using AssetManager.Api.Database;
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
            DatabaseQueryMultiResult<IList<AssetTypeInfo>> result = this.Api.DataBase.GetAssetTypeInfo();

            return View( new AssetTypeInfoModel( result, this.Api ) );
        }

        public IActionResult List( string database, string assetTypeName )
        {
            if ( Guid.TryParse( database, out Guid databaseId ) )
            {
                IList<Asset> assets = this.Api.DataBase.GetAssets( databaseId, assetTypeName );
                AssetListModel model = new AssetListModel( this.Api, assetTypeName, assets );
                return View( model );
            }
            else
            {
                return BadRequest( "Invalid database ID: " + database );
            }
        }
    }
}