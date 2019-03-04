//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Api.Database
{
    public class DatabaseApi
    {
        // ---------------- Fields ----------------

        private readonly IDatabaseConfig databaseConfig;

        // ---------------- Constructor ----------------

        public DatabaseApi( IDatabaseConfig databaseConfig )
        {
            this.databaseConfig = databaseConfig;
        }

        // ---------------- Functions ----------------

        public void AddAssetType( AssetTypeBuilder builder )
        {
            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfig ) )
            {
                DateTime timestamp = DateTime.UtcNow;
                AssetType assetType = new AssetType
                {
                    Name = builder.Name,
                    CreationDate = timestamp,
                    ModifiedDate = timestamp
                };

                conn.AssetTypes.Add( assetType );

                foreach( AttributeBuilder key in builder.KeyValueAttributeKeys )
                {
                    KeyValueAttributeType keyValueAttributeType = new KeyValueAttributeType
                    {
                        Name = key.Key
                    };

                    conn.KeyValueAttributeTypes.Add( keyValueAttributeType );

                    AssetTypeKeyValueAttributesMap map = new AssetTypeKeyValueAttributesMap
                    {
                        AssetType = assetType,
                        KeyValueAttributeType = keyValueAttributeType,
                        AttributeType = key.Type
                    };

                    conn.AssetTypeKeyValueAttributesMaps.Add( map );
                }

                conn.SaveChanges();
            }
        }

        /// <summary>
        /// Generates an empty asset that has all of its attributes
        /// set to null, so that a user can fill them in and add it to the database.
        /// </summary>
        public Asset GenerateEmptyAsset( string assetTypeName )
        {
            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfig ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, assetTypeName );

                Asset asset = new Asset
                {
                    AssetType = assetTypeName
                };

                // Next, get all of the attributes that are associated with the asset type.
                IEnumerable<AssetTypeKeyValueAttributesMap> maps = this.GetAttributesAssociatedWithAssetType(
                    conn,
                    assetType
                );

                foreach( AssetTypeKeyValueAttributesMap map in maps )
                {
                    asset.AddEmptyKeyValue( map.KeyValueAttributeType.Name, map.AttributeType );
                }

                return asset;
            }
        }

        public void AddAsset( Asset asset )
        {
            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfig ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, asset.AssetType );

                DateTime timestamp = DateTime.UtcNow;
                AssetInstance assetInstance = new AssetInstance
                {
                    Name = asset.Name,
                    AssetType = assetType,
                    CreationDate = timestamp,
                    ModifiedDate = timestamp
                };

                // Next, get all of the attributes that are associated with the asset type.
                IEnumerable<AssetTypeKeyValueAttributesMap> maps = this.GetAttributesAssociatedWithAssetType(
                    conn,
                    assetType
                );

                foreach( AssetTypeKeyValueAttributesMap map in maps )
                {
                    AssetInstanceKeyValueAttributeValues value = new AssetInstanceKeyValueAttributeValues
                    {
                        AssetInstance = assetInstance,
                        Key = map.KeyValueAttributeType,
                        Value = asset.KeyValueAttributes[map.KeyValueAttributeType.Name].Serialize()
                    };

                    conn.AssetInstanceKeyValueAttributeValues.Add( value );
                }

                conn.AssetInstances.Add( assetInstance );

                conn.SaveChanges();
            }
        }

        private AssetType GetAssetType( DatabaseConnection conn, string assetTypeName )
        {
            AssetType assetType = conn.AssetTypes.FirstOrDefault( t => t.Name == assetTypeName );
            if( assetType == null )
            {
                throw new InvalidOperationException( "Can not find Asset Type '" + assetType + '"' );
            }

            return assetType;
        }

        private IEnumerable<AssetTypeKeyValueAttributesMap> GetAttributesAssociatedWithAssetType( DatabaseConnection conn, AssetType assetType )
        {
            return conn.AssetTypeKeyValueAttributesMaps
                .Include( nameof( AssetTypeKeyValueAttributesMap.KeyValueAttributeType ) )
                .Where( m => m.AssetType.Id == assetType.Id );
        }
    }
}
