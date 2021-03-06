﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AssetManager.Api.Attributes.Types;
using AssetManager.Api.Database.Tables;
using Microsoft.EntityFrameworkCore;
using SethCS.Exceptions;

namespace AssetManager.Api.Database
{
    public class DatabaseApi : IDatabaseApi
    {
        // ---------------- Fields ----------------

        private delegate TResult DatabaseAction<TResult>( Guid databaseId );

        private readonly Dictionary<Guid, IDatabaseConfig> databaseConfigs;

        // ---------------- Constructor ----------------

        public DatabaseApi( IList<IDatabaseConfig> databaseConfigs )
        {
            this.databaseConfigs = new Dictionary<Guid, IDatabaseConfig>();
            Dictionary<Guid, string> databaseNames = new Dictionary<Guid, string>();

            foreach ( IDatabaseConfig config in databaseConfigs )
            {
                if ( this.databaseConfigs.ContainsKey( config.DatabaseId ) )
                {
                    throw new ArgumentException( "Database ID " + config.DatabaseId + " already exists!" );
                }

                this.databaseConfigs.Add( config.DatabaseId, config );
                databaseNames.Add( config.DatabaseId, config.Name );
            }

            // Next, if there are any duplicate database names, append the GUID at the end of them
            // so it is easy to tell which is which.
            HashSet<Guid> foundDups = new HashSet<Guid>();
            foreach ( KeyValuePair<Guid, string> name in databaseNames )
            {
                IEnumerable<KeyValuePair<Guid, string>> matchedNames = databaseNames.Where(
                    d => ( d.Value == name.Value ) && ( d.Key.Equals( name.Key ) == false )
                );

                if ( matchedNames.Count() > 0 )
                {
                    foundDups.Add( name.Key );
                }
            }

            foreach ( Guid guid in foundDups )
            {
                databaseNames[guid] = databaseNames[guid] + " " + guid.ToString();
            }

            this.DatabaseNames = new ReadOnlyDictionary<Guid, string>( databaseNames );
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// A dictionary of all the database names, whose key is the database ID.
        /// </summary>
        public IReadOnlyDictionary<Guid, string> DatabaseNames { get; private set; }

        // ---------------- Functions ----------------

        public int AddAssetType( AssetTypeBuilder builder )
        {
            ArgumentChecker.IsNotNull( builder, nameof( builder ) );
            builder.Validate();

            Guid databaseId = builder.DatabaseId;
            this.GuidCheck( databaseId );

            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
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
                    AttributeKeys attributeKey = new AttributeKeys
                    {
                        Name = key.Key,
                        AttributeType = key.AttributeType
                    };

                    conn.AttributeKeys.Add( attributeKey );

                    AttributeProperties properties = new AttributeProperties
                    {
                        DefaultValue = key.SerializeDefaultValue(),
                        PossibleValues = key.SerializePossibleValues(),
                        Required = key.Required
                    };
                    conn.AttributeProperties.Add( properties );

                    AssetTypeAttributesMap map = new AssetTypeAttributesMap
                    {
                        AssetType = assetType,
                        AttributeKey = attributeKey,
                        AttributeProperties = properties
                    };

                    conn.AssetTypeAttributesMaps.Add( map );
                }

                conn.SaveChanges();

                return assetType.Id; // Must come after SaveChanges() so the ID gets updated.
            }
        }

        public void DeleteAsset( Guid databaseId, int assetTypeId, int assetId )
        {
            this.GuidCheck( databaseId );

            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
            {
                AssetType assetType = this.GetAssetType( conn, assetTypeId );

                AssetInstance asset = conn.AssetInstances.FirstOrDefault( a => a.Id == assetId );
                if ( asset == null )
                {
                    throw new ArgumentException( "Could not find asset with ID " + assetId + " in database " + this.DatabaseNames[databaseId] );
                }

                // First, we need to delete all values that are related to the asset from the database.
                IEnumerable<AssetInstanceAttributeValues> values = conn.AssetInstanceAttributeValues.Where( v => v.AssetInstance.Id == asset.Id );
                conn.AssetInstanceAttributeValues.RemoveRange( values );

                // Lastly, delete the asset itself.
                conn.AssetInstances.Remove( asset );

                conn.SaveChanges();
            }
        }

        public IAssetType GetAssetType( Guid databaseId, int assetTypeId )
        {
            this.GuidCheck( databaseId );

            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
            {
                return GetAssetType( conn, databaseId, assetTypeId );
            }
        }

        private IAssetType GetAssetType( DatabaseConnection conn, Guid databaseId, int assetTypeId )
        {
            // First, find the type of asset.
            AssetType assetType = this.GetAssetType( conn, assetTypeId );
            string assetTypeName = assetType.Name;

            IEnumerable<AssetTypeAttributesMap> maps = conn.AssetTypeAttributesMaps
                .Include( nameof( AssetTypeAttributesMap.AttributeKey ) )
                .Include( nameof( AssetTypeAttributesMap.AttributeProperties ) )
                .Where( m => m.AssetType.Id == assetType.Id );

            AssetTypeBuilder assetTypeBuilder = new AssetTypeBuilder( assetTypeName, databaseId );

            foreach ( AssetTypeAttributesMap map in maps )
            {
                IAttributeType attributeType = AttributeTypeFactory.CreateAttributeType( map.AttributeKey.AttributeType );
                attributeType.DeserializeDefaultValue( map.AttributeProperties.DefaultValue );
                attributeType.DeserializePossibleValues( map.AttributeProperties.PossibleValues );
                attributeType.Key = map.AttributeKey.Name;
                attributeType.Required = map.AttributeProperties.Required;

                assetTypeBuilder.AttributeTypes.Add( attributeType );
            }

            return assetTypeBuilder;
        }

        /// <summary>
        /// Generates an empty asset that has all of its attributes
        /// set to null, so that a user can fill them in and add it to the database.
        /// </summary>
        public Asset GenerateEmptyAsset( Guid databaseId, int assetTypeId )
        {
            this.GuidCheck( databaseId );

            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, assetTypeId );

                Asset asset = new Asset( databaseId )
                {
                    AssetType = assetType.Name
                };

                // Next, get all of the attributes that are associated with the asset type.
                IEnumerable<AssetTypeAttributesMap> maps = this.GetAttributesAssociatedWithAssetType(
                    conn,
                    assetType
                );

                foreach( AssetTypeAttributesMap map in maps )
                {
                    asset.AddEmptyAttribute( map.AttributeKey.Name, map.AttributeKey.AttributeType );
                }

                return asset;
            }
        }

        public int AddAsset( Asset asset )
        {
            ArgumentChecker.IsNotNull( asset, nameof( asset ) );

            GuidCheck( asset.DatabaseId );

            using( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[asset.DatabaseId] ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, asset.AssetType );

                // Ensure our asset is compatiable with our type.
                IAssetType fullAssetType = this.GetAssetType( conn, asset.DatabaseId, assetType.Id );
                fullAssetType.ValidateAsset( asset );

                DateTime timestamp = DateTime.UtcNow;
                AssetInstance assetInstance = new AssetInstance
                {
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
                        AttributeKey = map.AttributeKey,
                        Value = asset.Attributes[map.AttributeKey.Name].Serialize()
                    };

                    conn.AssetInstanceAttributeValues.Add( value );
                }

                conn.AssetInstances.Add( assetInstance );

                conn.SaveChanges();

                return assetInstance.Id; // Must come after SaveChanges() so the ID gets updated.
            }
        }

        public Asset GetAsset( Guid databaseId, int assetId )
        {
            GuidCheck( databaseId );
            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
            {
                AssetInstance assetInstance = conn.AssetInstances
                    .Include( nameof( AssetInstance.AssetType ) )
                    .FirstOrDefault( i => i.Id == assetId );

                if ( assetInstance == null )
                {
                    throw new ArgumentException(
                        "Can not find asset id " + assetId + " in database " + databaseId,
                        nameof( assetId )
                    );
                }

                return CreateAsset( conn, databaseId, assetInstance.AssetType.Name, assetInstance );
            }
        }

        public void UpdateAsset( int id, Asset asset )
        {
            ArgumentChecker.IsNotNull( asset, nameof( asset ) );

            GuidCheck( asset.DatabaseId );

            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[asset.DatabaseId] ) )
            {
                // First, find the type of asset.
                AssetType assetType = this.GetAssetType( conn, asset.AssetType );

                // Ensure our asset is compatiable with our type.
                IAssetType fullAssetType = this.GetAssetType( conn, asset.DatabaseId, assetType.Id );
                fullAssetType.ValidateAsset( asset );

                // Get the asset instance currently in the database.
                AssetInstance assetInstance = conn.AssetInstances.FirstOrDefault( i => i.Id == id );
                if ( assetInstance == null )
                {
                    throw new ArgumentException( "Can not find asset of ID " + id + " in database " + asset.DatabaseId );
                }

                // Update all values of attributes.
                IEnumerable<AssetInstanceAttributeValues> values = conn.AssetInstanceAttributeValues
                    .Include( nameof(AssetInstanceAttributeValues.AttributeKey ) )
                    .Where( v => v.AssetInstance.Id == assetInstance.Id );

                foreach ( AssetInstanceAttributeValues value in values )
                {
                    value.Value = asset.Attributes[value.AttributeKey.Name].Serialize();
                    conn.AssetInstanceAttributeValues.Update( value );
                }

                // Update modified timestamp in the database.
                DateTime timestamp = DateTime.UtcNow;
                assetInstance.ModifiedDate = timestamp;
                conn.AssetInstances.Update( assetInstance );

                conn.SaveChanges();
            }
        }

        public AssetListInfo GetAssetsOfType( Guid databaseId, int assetTypeId )
        {
            this.GuidCheck( databaseId );

            Dictionary<int, Asset> assets = new Dictionary<int, Asset>();

            using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
            {
                AssetType assetType = this.GetAssetType( conn, assetTypeId );

                // Get all of the asset instances that match the name we want.
                IEnumerable<AssetInstance> assetInstances = conn.AssetInstances.Where( a => a.AssetType.Id == assetTypeId );

                foreach ( AssetInstance assetInstance in assetInstances )
                {
                    Asset asset = this.CreateAsset( conn, databaseId, assetType.Name, assetInstance );
                    assets.Add( assetInstance.Id, asset );
                }

                return new AssetListInfo( databaseId, assets, assetType.Id, assetType.Name );
            }
        }

        private Asset CreateAsset( DatabaseConnection conn, Guid databaseId, string assetType, AssetInstance assetInstance )
        {
            // Next, get all of the attributes and their values associated with the asset instance.
            IEnumerable<AssetInstanceAttributeValues> attributeValues = conn.AssetInstanceAttributeValues
                .Include( nameof( AssetInstanceAttributeValues.AssetInstance ) )
                .Include( nameof( AssetInstanceAttributeValues.AttributeKey ) )
                .Where( i => i.AssetInstance.Id == assetInstance.Id );

            // Create the asset.
            Asset asset = new Asset( databaseId )
            {
                AssetType = assetType
            };

            foreach ( AssetInstanceAttributeValues value in attributeValues )
            {
                IAttribute attr = AttributeFactory.CreateAttribute( value.AttributeKey.AttributeType );
                attr.Deserialize( value.Value );

                asset.AddEmptyAttribute( value.AttributeKey.Name, attr.AttributeType );
                asset.SetAttribute( value.AttributeKey.Name, attr );
            }

            return asset;
        }

        public DatabaseQueryMultiResult<IList<AssetType>> GetAssetTypeNames()
        {
            return this.PerformActionOnDatabases<IList<AssetType>>(
                delegate ( Guid databaseId )
                {
                    using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
                    {
                        return conn.AssetTypes.OrderBy( a => a.Name ).ToList();
                    }
                }
            );
        }

        public DatabaseQueryMultiResult<IList<AssetTypeInfo>> GetAssetTypeInfo()
        {
            return this.PerformActionOnDatabases<IList<AssetTypeInfo>>(
                delegate ( Guid databaseId )
                {
                    List<AssetTypeInfo> infoList = new List<AssetTypeInfo>();

                    using ( DatabaseConnection conn = new DatabaseConnection( this.databaseConfigs[databaseId] ) )
                    {
                        IEnumerable<AssetType> assetTypes = conn.AssetTypes.Select( n => n );
                        foreach ( AssetType type in assetTypes )
                        {
                            IEnumerable<int> assets =
                                conn.AssetInstances.Include( nameof( AssetInstance.AssetType ) )
                                .Select( a => a.AssetType.Id )
                                .Where( id => id == type.Id );

                            AssetTypeInfo info = new AssetTypeInfo(
                                type.Id,
                                type.Name,
                                databaseId,
                                assets.Count(),
                                this.DatabaseNames[databaseId]
                            );
                            infoList.Add( info );
                        }
                    }

                    return infoList;
                }
            );
        }

        private AssetType GetAssetType( DatabaseConnection conn, int assetTypeId )
        {
            AssetType assetType = conn.AssetTypes.SingleOrDefault( t => t.Id == assetTypeId );
            if ( assetType == null )
            {
                throw new InvalidOperationException( "Can not find Asset Type with ID '" + assetTypeId + '"' );
            }

            return assetType;
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
                .Include( nameof( AssetTypeAttributesMap.AttributeKey ) )
                .Where( m => m.AssetType.Id == assetType.Id );
        }

        private void GuidCheck( Guid databaseId )
        {
            if ( this.databaseConfigs.ContainsKey( databaseId ) == false )
            {
                throw new ArgumentException( "Database ID " + databaseId + " does not exist!", nameof( databaseId ) );
            }
        }

        private DatabaseQueryMultiResult<TResult> PerformActionOnDatabases<TResult>( DatabaseAction<TResult> action )
        {
            IEnumerable<Guid> databaseIds = this.databaseConfigs.Keys;
            Dictionary<Guid, Task<TResult>> tasks = new Dictionary<Guid, Task<TResult>>();

            Dictionary<Guid, DatabaseQueryResult<TResult>> results = new Dictionary<Guid, DatabaseQueryResult<TResult>>();

            try
            {
                foreach ( Guid guid in databaseIds )
                {
                    Guid innerGuid = guid;
                    Task<TResult> task = Task.Factory.StartNew( () => action( guid ) );
                    tasks.Add( guid, task );
                }
            }
            finally
            {
                foreach ( KeyValuePair<Guid, Task<TResult>> task in tasks )
                {
                    DatabaseQueryResult<TResult> queryResult;
                    try
                    {
                        task.Value.Wait();
                        TResult result = task.Value.Result;
                        queryResult = new DatabaseQueryResult<TResult>( task.Key, result, null );
                    }
                    catch ( Exception e )
                    {
                        queryResult = new DatabaseQueryResult<TResult>( task.Key, default, e );
                    }

                    results[task.Key] = queryResult;
                }
            }

            return new DatabaseQueryMultiResult<TResult>( results );
        }
    }
}
