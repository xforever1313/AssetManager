//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    public class DatabaseApi
    {
        // ---------------- Fields ----------------

        // ---------------- Constructor ----------------

        public DatabaseApi()
        {
        }

        // ---------------- Functions ----------------

        public void AddAssetType( AssetTypeBuilder builder )
        {
            using( DatabaseConnection conn = new DatabaseConnection() )
            {
                AssetType assetType = new AssetType
                {
                    Name = builder.Name
                };

                conn.AssetTypes.Add( assetType );

                foreach( string key in builder.KeyValueAttributeKeys )
                {
                    KeyValueAttributeType keyValueAttributeType = new KeyValueAttributeType
                    {
                        Name = key
                    };

                    conn.KeyValueAttributeTypes.Add( keyValueAttributeType );

                    AssetTypeKeyValueAttributesMap map = new AssetTypeKeyValueAttributesMap
                    {
                        AssetType = assetType,
                        KeyValueAttributeType = keyValueAttributeType
                    };

                    conn.AssetTypeKeyValueAttributesMaps.Add( map );
                }

                conn.SaveChanges();
            }
        }

        public void AddAsset( Asset asset )
        {
            using( DatabaseConnection conn = new DatabaseConnection() )
            {
                // First, find the type of asset.
                AssetType assetType = conn.AssetTypes.FirstOrDefault( t => t.Name == asset.AssetType );
                if( assetType == null )
                {
                    throw new InvalidOperationException( "Can not find Asset Type '" + asset.AssetType + '"' );
                }

                AssetInstance assetInstance = new AssetInstance
                {
                    Name = asset.Name,
                    AssetType = assetType
                };

                // Next, get all of the attributes that are associated with the asset type.
                IEnumerable<AssetTypeKeyValueAttributesMap> maps = conn.AssetTypeKeyValueAttributesMaps.Include( "KeyValueAttributeType" ).Where( m => m.AssetType.Id == assetType.Id );
                foreach( AssetTypeKeyValueAttributesMap map in maps )
                {
                    AssetInstanceKeyValueAttributeValues value = new AssetInstanceKeyValueAttributeValues
                    {
                        AssetInstance = assetInstance,
                        Key = map.KeyValueAttributeType,
                        Value = asset.KeyValueAttributes[map.KeyValueAttributeType.Name]
                    };

                    conn.AssetInstanceKeyValueAttributeValues.Add( value );
                }

                conn.AssetInstances.Add( assetInstance );

                conn.SaveChanges();
            }
        }
    }
}
