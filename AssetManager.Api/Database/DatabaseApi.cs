﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Api.Attributes.Types;
using Microsoft.EntityFrameworkCore;
using SethCS.Exceptions;

namespace AssetManager.Api.Database
{
    public class DatabaseApi : IDatabaseApi
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
            ArgumentChecker.IsNotNull( builder, nameof( builder ) );
            builder.Validate();

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

                foreach( IAttributeType key in builder.AttributeTypes )
                {
                    AttributeNames keyValueAttributeType = new AttributeNames
                    {
                        Name = key.Key
                    };

                    conn.AttributeNames.Add( keyValueAttributeType );

                    AssetTypeAttributesMap map = new AssetTypeAttributesMap
                    {
                        AssetType = assetType,
                        AttributeName = keyValueAttributeType,
                        AttributeType = key.AttributeType
                    };

                    conn.AssetTypeAttributesMaps.Add( map );
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
            ArgumentChecker.StringIsNotNullOrEmpty( assetTypeName, nameof( assetTypeName ) );

            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfig ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, assetTypeName );

                Asset asset = new Asset
                {
                    AssetType = assetTypeName
                };

                // Next, get all of the attributes that are associated with the asset type.
                IEnumerable<AssetTypeAttributesMap> maps = this.GetAttributesAssociatedWithAssetType(
                    conn,
                    assetType
                );

                foreach( AssetTypeAttributesMap map in maps )
                {
                    asset.AddEmptyKeyValue( map.AttributeName.Name, map.AttributeType );
                }

                return asset;
            }
        }

        public void AddAsset( Asset asset )
        {
            ArgumentChecker.IsNotNull( asset, nameof( asset ) );

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
                IEnumerable<AssetTypeAttributesMap> maps = this.GetAttributesAssociatedWithAssetType(
                    conn,
                    assetType
                );

                foreach( AssetTypeAttributesMap map in maps )
                {
                    AssetInstanceAttributeValues value = new AssetInstanceAttributeValues
                    {
                        AssetInstance = assetInstance,
                        AttributeName = map.AttributeName,
                        Value = asset.KeyValueAttributes[map.AttributeName.Name].Serialize()
                    };

                    conn.AssetInstanceAttributeValues.Add( value );
                }

                conn.AssetInstances.Add( assetInstance );

                conn.SaveChanges();
            }
        }

        public IList<string> GetAssetTypeNames()
        {
            List<string> names;

            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfig ) )
            {
                names = new List<string>(
                    conn.AssetTypes.Select( a => a.Name )
                    .OrderBy( a => a )
                );
            }

            return names;
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

        private IEnumerable<AssetTypeAttributesMap> GetAttributesAssociatedWithAssetType( DatabaseConnection conn, AssetType assetType )
        {
            return conn.AssetTypeAttributesMaps
                .Include( nameof( AssetTypeAttributesMap.AttributeName ) )
                .Where( m => m.AssetType.Id == assetType.Id );
        }
    }
}
