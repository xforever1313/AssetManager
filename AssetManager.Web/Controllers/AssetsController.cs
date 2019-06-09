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

                // TODO: Should we make this one query instead of 2?
                IAssetType assetType = this.Api.DataBase.GetAssetType( databaseId, assetTypeId );
                AssetModel assetModel = new AssetModel( this.Api, emptyAsset, assetTypeId, assetType );
                return View( assetModel );
            }
            else
            {
                return BadRequest( "Invalid database ID: " + database );
            }
        }

        [HttpPost]
        public IActionResult Add( string database, int assetTypeId, [FromBody] AssetBuilderModel assetBuilder )
        {
            try
            {
                if ( Guid.TryParse( database, out Guid databaseId ) )
                {
                    if ( assetBuilder.Success )
                    {
                        Asset asset = this.Api.DataBase.GenerateEmptyAsset( databaseId, assetTypeId );
                        foreach ( KeyValuePair<string, IAttribute> attribute in assetBuilder.Attributes )
                        {
                            asset.SetAttribute( attribute.Key, attribute.Value );
                        }
                        this.Api.DataBase.AddAsset( asset );

                        return Ok();
                    }
                    else
                    {
                        return BadRequest( assetBuilder.ErrorMessage );
                    }
                }
                else
                {
                    return BadRequest( "Invalid database ID: " + database );
                }
            }
            catch ( Exception e )
            {
                return BadRequest( e.Message );
            }
        }

        [HttpPost]
        public IActionResult Delete( string database, int assetTypeId, int assetId )
        {
            try
            {
                Guid databaseGuid = Guid.Parse( database );
                this.Api.DataBase.DeleteAsset( databaseGuid, assetTypeId, assetId );

                return Redirect( "/Assets/List/" + database + "/" + assetTypeId );
            }
            catch ( Exception e )
            {
                // TODO: Return an error view?
                return BadRequest( e.Message );
            }
        }
    }
}