using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManager.Api;
using AssetManager.Api.Database;

namespace AssetManager.Web.Models
{
    public class AssetTypeInfoModel : IModel
    {
        // ---------------- Constructor ----------------

        public AssetTypeInfoModel( DatabaseQueryMultiResult<IList<AssetTypeInfo>> queryResult, IAssetManagerApi api )
        {
            this.Api = api;

            List<AssetTypeInfo> infoList = new List<AssetTypeInfo>();
            List<string> errorList = new List<string>();

            foreach ( DatabaseQueryResult<IList<AssetTypeInfo>> info in queryResult.Results.Values )
            {
                if ( info.Success )
                {
                    foreach ( AssetTypeInfo assetTypeInfo in info.Result )
                    {
                        infoList.Add( assetTypeInfo );
                    }
                }
                else
                {
                    errorList.Add( api.DataBase.DatabaseNames[info.DatabaseId] + ": " + info.Error.Message );
                }
            }

            this.AssetTypeInfo = infoList.AsReadOnly();
            this.Errors = errorList.AsReadOnly();
        }

        // ---------------- Properties ----------------

        public IReadOnlyList<AssetTypeInfo> AssetTypeInfo { get; private set; }

        public IAssetManagerApi Api { get; private set; }

        public IReadOnlyList<string> Errors { get; private set; }
    }
}
