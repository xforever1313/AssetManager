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

        public IActionResult List( string database, int assetTypeId )
        {
            if ( Guid.TryParse( database, out Guid databaseId ) )
            {
                AssetListInfo assets = this.Api.DataBase.GetAssetsOfType( databaseId, assetTypeId );
                AssetListModel model = new AssetListModel( this.Api, assets );
                return View( model );
            }
            else
            {
                return BadRequest( "Invalid database ID: " + database );
            }
        }

        public IActionResult Add( string database, int assetTypeId )
        {
            if ( Guid.TryParse( database, out Guid databaseId ) )
            {
                Asset emptyAsset = this.Api.DataBase.GenerateEmptyAsset( databaseId, assetTypeId );
                AssetModel assetModel = new AssetModel( this.Api, emptyAsset, assetTypeId );
                return View( assetModel );
            }
            else
            {
                return BadRequest( "Invalid database ID: " + database );
            }
        }
    }
}